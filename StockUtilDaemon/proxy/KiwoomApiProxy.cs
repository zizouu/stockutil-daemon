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

        public void getDailyBatchData()
        {
            LogUtil.logConsole("Proxy getDailyBatchData start");
            Dictionary<String, Dictionary<String, String>>  codeDic = getValueableCodeList();
            Dictionary<String, String> kospiDic, kosdaqDic;
            codeDic.TryGetValue("0", out kospiDic);
            codeDic.TryGetValue("10", out kosdaqDic);
            LogUtil.logConsole("KOSPI Count : " + kosdaqDic.Count);
            List<DateTime> period = DateUtil.getPeriodDateTimeExceptWeekend(DateTime.Now, 0);
            LogUtil.logConsole("Date Count : " + period.Count);
            foreach(KeyValuePair<String, String> kvp in kospiDic)
            {
                foreach(DateTime date in period){
                    LogUtil.logConsole("Proxy call request");
                    requestStockDataByCode(kvp.Key, DateUtil.convertDateTimeToString(date));
                }   
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

            Dictionary<String, Dictionary<String, String>> codeListDic = new Dictionary<String, Dictionary<String, String>>();
            codeListDic.Add("0", kospiDic);
            codeListDic.Add("3", elwDic);
            codeListDic.Add("4", mutualFundDic);
            codeListDic.Add("5", newStockDic);
            codeListDic.Add("6", reitDic);
            codeListDic.Add("8", etfDic);
            codeListDic.Add("9", highYieldDic);
            codeListDic.Add("10", kosdaqDic);

            removeCodeByMarketNum(codeListDic, "0", "4");
            removeCodeByMarketNum(codeListDic, "0", "8");
            
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
        
        

        private void requestStockDataByCode(String code, String date)
        {
            kiWoomApi.SetInputValue("종목코드", code);
            kiWoomApi.SetInputValue("기준일자", date);
            kiWoomApi.SetInputValue("수정주가구분", "0");
            kiWoomApi.CommRqData("일봉조회", "opt10081", 0, "1002");
        }

        private void removeCodeByMarketNum(Dictionary<String, Dictionary<String, String>> codeDic, String fromMarketNum, String toMarketNum)
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
