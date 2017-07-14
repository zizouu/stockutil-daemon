using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockUtilDaemon
{
    class DailyTradingModel
    {
        private String code { get;}
        private String name { get;}
        private int startPrice { get; }
        private int endPrice { get; }
        private int highPrice { get; }
        private int lowPrice { get; }
        private int volume { get; }
        private int tradingValue { get; }
        private DateTime tradingDate { get; }
        private DateTime createdDate { get; }
        private DateTime modifiedDate { get; }

        public DailyTradingModel(String code, String name, int startPrice, int endPrice, int highPrice, int lowPrice, int volume, int tradingValue, 
                                DateTime tradingDate, DateTime createdDate, DateTime modifiedDate)
        {
            this.code = code;
            this.name = name;
            this.startPrice = startPrice;
            this.endPrice = endPrice;
            this.highPrice = highPrice;
            this.lowPrice = lowPrice;
            this.volume = volume;
            this.tradingValue = tradingValue;
            this.tradingDate = tradingDate;
            this.createdDate = createdDate;
            this.modifiedDate = modifiedDate;
        }
    }
}
