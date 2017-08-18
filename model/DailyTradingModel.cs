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

        public String Code { get { return code; } }
        public String Name { get { return name; } }
        public int StartPrice { get { return startPrice; } }
        public int EndPrice { get { return endPrice; } }
        public int HighPrice { get { return highPrice; } }
        public int LowPrice { get { return lowPrice; } }
        public int Volume { get { return volume; } }
        public int TradingValue { get { return tradingValue; } }
        public DateTime TradingDate { get { return tradingDate; } }
        public DateTime CreatedDate { get { return createdDate; } }
        public DateTime ModifiedDate { get { return modifiedDate; } }
    }
}
