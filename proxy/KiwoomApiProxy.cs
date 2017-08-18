using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockUtilDaemon
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////                                                                                                       //// 
    ////                                            Market Code                                                //// 
    ////   0 : 장내, 3 : ELW, 4 : 뮤추얼펀드, 5 : 신주인수권, 6 : 리츠, 8 : ETF, 9 : 하이일펀드, 10 : 코스닥   ////
    ////                                                                                                       ////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

    class KiwoomApiProxy
    {
        private AxKHOpenAPILib.AxKHOpenAPI kiWoomApi;

        public KiwoomApiProxy(AxKHOpenAPILib.AxKHOpenAPI api)
        {
            this.kiWoomApi = api;

        }

        public Dictionary<String, Dictionary<String, String>> getBatchCodeAndNameData()
        {
            Dictionary<String, Dictionary<String, String>> allStockCodeDic = getValueableCodeList();
            Dictionary<String, Dictionary<String, String>> kospiAndKosdaq = new Dictionary<String, Dictionary<String, String>>();
            Dictionary<String, String> kospiDic, kosdaqDic;

            allStockCodeDic.TryGetValue("0", out kospiDic);
            allStockCodeDic.TryGetValue("10", out kosdaqDic);
            
            kospiAndKosdaq.Add("0", kospiDic);
            kospiAndKosdaq.Add("10", kosdaqDic);

            return kospiAndKosdaq;
        }

        public List<StockItemModel> convertCodeNameToModel(Dictionary<String, String> dic)
        {
            List<StockItemModel> list = new List<StockItemModel>();
            //foreach(KeyValuePair kvp in )
            return new List<StockItemModel>();
        }

        public void getDailyBatchData(Dictionary<String, Dictionary<String, String>> codeDic)
        {
            LogUtil.logConsole("Proxy getDailyBatchData start");
            Dictionary<String, String> kospiDic, kosdaqDic;
            codeDic.TryGetValue("0", out kospiDic);
            codeDic.TryGetValue("10", out kosdaqDic);

            foreach (KeyValuePair<String, String> kvp in kospiDic)
            {
                requestStockDataByCode(kvp.Key, DateUtil.convertDateTimeToString(DateTime.Now));
            }
            LogUtil.logConsole("Proxy getDailyBatchData stop");
        }

        private Dictionary<String, Dictionary<String, String>> getValueableCodeList()
        {
            Dictionary<String, String> kospiDic = getCodeDictionaryByMarketNum("0");
            Dictionary<String, String> elwDic = getCodeDictionaryByMarketNum("3");
            Dictionary<String, String> mutualFundDic = getCodeDictionaryByMarketNum("4");
            Dictionary<String, String> newStockDic = getCodeDictionaryByMarketNum("5");
            Dictionary<String, String> reitDic = getCodeDictionaryByMarketNum("6");
            Dictionary<String, String> etfDic = getCodeDictionaryByMarketNum("8");
            Dictionary<String, String> highYieldDic = getCodeDictionaryByMarketNum("9");
            Dictionary<String, String> kosdaqDic = getCodeDictionaryByMarketNum("10");

            LogUtil.logConsole("kospi dic count : " + kospiDic.Count);
            
            Dictionary<String, Dictionary<String, String>> codeListDic = new Dictionary<String, Dictionary<String, String>>();
            codeListDic.Add("0", kospiDic);
            codeListDic.Add("3", elwDic);
            codeListDic.Add("4", mutualFundDic);
            codeListDic.Add("5", newStockDic);
            codeListDic.Add("6", reitDic);
            codeListDic.Add("8", etfDic);
            codeListDic.Add("9", highYieldDic);
            codeListDic.Add("10", kosdaqDic);

            removeDuplicatedCodeByMarketNum(codeListDic, "0", "4");
            removeDuplicatedCodeByMarketNum(codeListDic, "0", "8");
            
            return codeListDic;
        }

        private Dictionary<String, String> getCodeDictionaryByMarketNum(String marketNum)
        {
            DataUtil util = new DataUtil();
            String codeData = kiWoomApi.GetCodeListByMarket(marketNum);
            List<String> codeList = util.parseCodeData(codeData);
            Dictionary<String, String> codeDic = new Dictionary<String, String>();
            codeList.RemoveAt(codeList.Count - 1);

            foreach (String code in codeList)
            {
                String name = kiWoomApi.GetMasterCodeName(code);
                if (marketNum.Equals("0") && code[0] == '5')
                {
                    continue;
                }
                codeDic.Add(code, name);
            }
            return codeDic;
        }

        private String getMarketName(String marketNum)
        {
            String marketName;
            switch (marketNum)
            {
                case "0":
                    marketName = "KOSPI";
                    break;
                case "3":
                    marketName = "ELW";
                    break;
                case "4":
                    marketName = "뮤추얼펀드";
                    break;
                case "5":
                    marketName = "신주인수권";
                    break;
                case "6":
                    marketName = "리츠";
                    break;
                case "8":
                    marketName = "ETF";
                    break;
                case "9":
                    marketName = "하이일드펀드";
                    break;
                case "10":
                    marketName = "코스닥";
                    break;
                default:
                    marketName = "NULL";
                    break;
            }
            return marketName;
        }
        
        public void getDailyDataForTest(String code, String date)
        {
            requestStockDataByCode(code, date);
        }

        public void requestStockDataByCode(String code, String date)
        {
            kiWoomApi.SetInputValue("종목코드", code);
            kiWoomApi.SetInputValue("기준일자", date);
            kiWoomApi.SetInputValue("수정주가구분", "0");
            kiWoomApi.CommRqData("일봉조회", "opt10081", 0, "1002");
        }

        private void removeDuplicatedCodeByMarketNum(Dictionary<String, Dictionary<String, String>> codeDic, String fromMarketNum, String toMarketNum)
        {
            Dictionary<String, String> fromDic = codeDic[fromMarketNum];
            Dictionary<String, String> toDic = codeDic[toMarketNum];

            foreach (KeyValuePair<String, String> data in toDic)
            {
                fromDic.Remove(data.Key);
            }
        }
    }
}
