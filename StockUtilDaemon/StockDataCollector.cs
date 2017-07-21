using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockUtilDaemon
{
    class StockDataCollector
    {
        private KiwoomApiProxy proxy;
        private AxKHOpenAPILib.AxKHOpenAPI kiWoomApi;
        private DatabaseController dbController;

        public StockDataCollector(AxKHOpenAPILib.AxKHOpenAPI api)
        {
            LogUtil.logConsole("StockDataCollector constructor start");
            this.proxy = new KiwoomApiProxy(api);
            this.kiWoomApi = api;
            //this.dbController = new DatabaseController();
            this.kiWoomApi.OnReceiveTrData += this.onReceiveKiwoomApiEvent;
            LogUtil.logConsole("StockDataCollector constructor end");
        }

        public void startCollect()
        {
            proxy.getDailyBatchData();
        }

        private void onReceiveKiwoomApiEvent(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            LogUtil.logConsole("onReceiveKiwoomApiEvent start");
            if (e.sRQName == "일봉조회")
            {
                int cnt = kiWoomApi.GetRepeatCnt(e.sTrCode, e.sRQName);
                for (int idx = 0; idx < cnt; idx++)
                {
                    Console.WriteLine("=================================================================================");
                    Console.WriteLine("============================ Get Data By OpenApi ================================");
                    Console.WriteLine("종목코드" + getReceiveDataByName(e, idx, "종목코드"));
                    Console.WriteLine("시가" + getReceiveDataByName(e, idx, "시가"));
                    Console.WriteLine("현재가" + getReceiveDataByName(e, idx, "현재가"));
                    Console.WriteLine("거래대금" + getReceiveDataByName(e, idx, "거래대금"));
                    Console.WriteLine("일자" + getReceiveDataByName(e, idx, "일자"));
                    Console.WriteLine("고가" + getReceiveDataByName(e, idx, "고가"));
                    Console.WriteLine("저가" + getReceiveDataByName(e, idx, "저가"));
                    Console.WriteLine("종목정보" + getReceiveDataByName(e, idx, "종목정보"));
                    Console.WriteLine("대업종구분" + getReceiveDataByName(e, idx, "대업종구분"));
                    Console.WriteLine("소업종구분" + getReceiveDataByName(e, idx, "소업종구분"));
                    Console.WriteLine("=============================== End Get Data ====================================");
                    Console.WriteLine("=================================================================================");
                }
            }
            LogUtil.logConsole("onReceiveKiwoomApiEvent end");
        }

        private String getReceiveDataByName(AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e, int index, String name)
        {
            return kiWoomApi.GetCommData(e.sTrCode, e.sRQName, index, name);
        }
    }
}
