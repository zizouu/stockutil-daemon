﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockUtilDaemon
{
    class StockDataCollector
    {
        public static String STATUS_NONE = "none";
        public static String STATUS_READY = "ready";
        public static String STATUS_CHECK_ITEM = "check item";
        public static String STATUS_COLLECT = "collect data";
        public static String STATUS_FINISH = "finish";
        public static bool isLockApi = false;

        private String TEST_STOCK_CODE = "009680";  // 모토닉 코드번호
        private String TEST_DATE = "20170804";

        private KiwoomApiProxy proxy;
        private AxKHOpenAPILib.AxKHOpenAPI kiWoomApi;
        private DatabaseController dbController;

        private Dictionary<String, Dictionary<String, String>> codeDic;
        private String lastUpdate;
        private String collectorStatus = STATUS_NONE;

        public StockDataCollector(AxKHOpenAPILib.AxKHOpenAPI api)
        {
            this.proxy = new KiwoomApiProxy(api);
            this.kiWoomApi = api;
            this.dbController = new DatabaseController();
            this.kiWoomApi.OnReceiveTrData += this.onReceiveKiwoomApiEvent;
            this.collectorStatus = STATUS_READY;
        }

        public String getCollectorStatus()
        {
            return collectorStatus;
        }

        public void startCollect()
        {
            this.collectorStatus = STATUS_CHECK_ITEM;   
            this.codeDic = proxy.getBatchCodeAndNameData();
            updateStockItem();
            checkLastUpdateDate();
            this.collectorStatus = STATUS_COLLECT;
            proxy.getDailyBatchData(this.codeDic);
            updateLastUpdateDate();
            this.collectorStatus = STATUS_FINISH;
            ////proxy.getDailyDataForTest(this.TEST_STOCK_CODE, this.TEST_DATE);
        }

        private void updateLastUpdateDate()
        {
            if ("FIRST".Equals(this.lastUpdate))
            {
                dbController.insertLastUpdateDate();
            }
            else
            {
                dbController.updateLastUpdateDate();
            }
            
        }

        private void checkLastUpdateDate()
        {
            String lastUpdateDate = dbController.selectLastUpdateDate();
            if("FIRST".Equals(lastUpdateDate))
            {
                this.lastUpdate = "FIRST";
                LogUtil.logConsole("database is update first time");
            }
            else
            {
                this.lastUpdate = lastUpdateDate;
                LogUtil.logConsole("last update date is " + lastUpdateDate);
            }
        }

        private void updateStockItem()
        {
            Dictionary<String, String> kospiDic, kosdaqDic;
            this.codeDic.TryGetValue("0", out kospiDic);
            this.codeDic.TryGetValue("10", out kosdaqDic);

            Dictionary<String, String> itemDic = dbController.selectAllStockItem();
            checkAndInsertNewItem(kospiDic, itemDic);
            checkAndInsertNewItem(kosdaqDic, itemDic);
        }

        private void checkAndInsertNewItem(Dictionary<String, String> proxyDic, Dictionary<String, String> dbDic)
        {
            foreach (KeyValuePair<String, String> kvp in proxyDic)
            {
                if (!dbDic.ContainsKey(kvp.Key))
                {
                    LogUtil.logConsole("key : " + kvp.Key + ", value : " + kvp.Value);
                    dbController.insertStockItem(new StockItemModel(kvp.Key, kvp.Value));
                }
            }
        }

        private void onReceiveKiwoomApiEvent(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            if (e.sRQName == "일봉조회")
            {
                StockDataCollector.isLockApi = true;
                int cnt = kiWoomApi.GetRepeatCnt(e.sTrCode, e.sRQName);
                if(cnt == 0)
                {
                    LogUtil.logConsole(" count zero!! ");
                }
                else
                {
                    String code = getReceiveDataCode(e);
                    List<DailyTradingModel> list = new List<DailyTradingModel>();

                    // if most recent data is inserted then skip logic
                    DailyTradingModel recentModel = makeDailyTradingModel(e, 0, code);
                    if (!checkIsInsertedData(code, recentModel))
                    {
                        DateTime recentDate = dbController.selectRecentTradingDate(code);
                        DailyTradingModel firstModel = makeDailyTradingModel(e, cnt - 1, code);
                        DailyTradingModel lastModel = makeDailyTradingModel(e, 0, code);

                        bool isFirstInserted = compareRecentDateAndModelDate(recentDate, firstModel.TradingDate);
                        bool isLastInserted = compareRecentDateAndModelDate(recentDate, lastModel.TradingDate);

                        for (int idx = 0; idx < cnt; idx++)
                        {
                            DailyTradingModel model = makeDailyTradingModel(e, (cnt - 1) - idx, code);
                            bool isInserted = compareRecentDateAndModelDate(recentDate, model.TradingDate);
                            if (!isInserted)
                            {
                                list.Add(model);
                            }
                            else if(isFirstInserted && isLastInserted)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        LogUtil.logConsole("SKIP : " + code);
                    }

                    if (list.Count != 0)
                    {
                        dbController.insertDailyBatchData(list);
                        LogUtil.logConsole(" db inserteds ");
                    }
                }
                StockDataCollector.isLockApi = false;
            }
        }

        private DateTime getRecentTradingDateByCode(String code)
        {
            return dbController.selectRecentTradingDate(code);
        }

        private bool compareRecentDateAndModelDate(DateTime recentDate, DateTime modelDate)
        {
            int result = DateTime.Compare(recentDate, modelDate);
            return (result < 0) ? false : true;
        }

        private bool checkIsInsertedData(String code, DailyTradingModel model)
        {
            DateTime recentDate = getRecentTradingDateByCode(code);
            return compareRecentDateAndModelDate(recentDate, model.TradingDate);
        }

        private bool isNewData(DailyTradingModel model)
        {
            if ("FIRST".Equals(this.lastUpdate)){
                return true;
            }
            DateTime last = DateUtil.convertStringToDateTime(this.lastUpdate);
            int result = DateTime.Compare(last, model.TradingDate);

            return (result < 0) ? true : false;
        }

        private String getReceiveDataCode(AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            
            return DateUtil.trimWhiteSpace(getReceiveDataByName(e, 0, "종목코드"));
        }

        private String getReceiveDataByName(AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e, int index, String name)
        {
            return kiWoomApi.GetCommData(e.sTrCode, e.sRQName, index, name);
        }

        private DailyTradingModel makeDailyTradingModel(AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e, int index, String code)
        {
            String name = getStockNameByCode(code);
            int startPrice = int.Parse(getReceiveDataByName(e, index, "시가"));
            int endPrice = int.Parse(getReceiveDataByName(e, index, "현재가"));
            int highPrice = int.Parse(getReceiveDataByName(e, index, "고가"));
            int lowPrice = int.Parse(getReceiveDataByName(e, index, "저가"));
            int volume = int.Parse(getReceiveDataByName(e, index, "거래량"));
            int tradingValue = int.Parse(getReceiveDataByName(e, index, "거래대금"));
            DateTime tradingDate = DateUtil.convertStringToDateTime(getReceiveDataByName(e, index, "일자"));
            DateTime createdDate = DateUtil.convertStringToDateTime(DateTime.Now.ToShortDateString());
            DateTime modifiedDate = DateUtil.convertStringToDateTime(DateTime.Now.ToShortDateString());

            return new DailyTradingModel(code, name, startPrice, endPrice, highPrice, lowPrice, volume, tradingValue, tradingDate,
                                            createdDate, modifiedDate);

        }

        private String getStockNameByCode(String code)
        {
            Dictionary<String, String> kospiDic, kosdaqDic;
            this.codeDic.TryGetValue("0", out kospiDic);
            this.codeDic.TryGetValue("10", out kosdaqDic);
            bool isFindCode = false;
            String name = "NULL";

            foreach(KeyValuePair<String, String> kvp in kospiDic)
            {
                if (kvp.Key.Equals(code))
                {
                    name = kvp.Value;
                    isFindCode = true;
                    break;
                }
            }

            if (!isFindCode)
            {
                foreach (KeyValuePair<String, String> kvp in kosdaqDic)
                {
                    if (kvp.Key.Equals(code))
                    {
                        name = kvp.Value;
                        isFindCode = true;
                        break;
                    }
                }
            }

            return name;
        }
    }
}
