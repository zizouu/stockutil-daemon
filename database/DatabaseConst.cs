using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockUtilDaemon
{
    class DatabaseConst
    {
        //public static String DATABASE_IP = "loacalhost";
        //public static String DATABASE_NAME = "stock_test";
        //public static String DATABASE_ID = "postgres";
        //public static String DATABASE_PW = "h382612";
        public static String DATABASE_IP = "54.190.58.114";
        public static String DATABASE_NAME = "stock";
        public static String DATABASE_ID = "stock_admin";
        public static String DATABASE_PW = "!Han382612";

        public static String CONFIG_NAME_LAST_UPDATE = "last_update_date";

        public static String SELECT_ALL_DAILY_TRADING = "SELECT * FROM daily_trading";
        public static String SELECT_ALL_STOCK_ITEM = "SELECT * FROM stock_item";
        public static String SELECT_LAST_UPDATE_DATE = "SELECT * FROM daemon_config WHERE config_name = 'last_update_date'";

        public static String UPDATE_LAST_UPDATE_DATE = "UPDATE daemon_config SET value = @value WHERE config_name = 'last_update_date'";

        public static String INSERT_LAST_UPDATE_DATE = "INSERT INTO daemon_config (config_name, value) " +
                                                            "VALUES (@configName, @value)";
        public static String INSERT_STOCK_ITEM = "INSERT INTO stock_item (code, name) " +
                                                            "VALUES (@code, @name)";
        public static String INSERT_DAILY_TRADING = "INSERT INTO daily_trading (code, name, start_price, end_price, " +
                                                                        "high_price, low_price, volume, trading_value, " +
                                                                        "trading_date, created_date, modified_date) " +
                                                            "VALUES (@code, @name, @startPrice, @endPrice, " +
                                                                    "@highPrice, @lowPrice, @volume, @tradingValue, " +
                                                                    "@tradingDate, @createdDate, @modifiedDate)";
    }
}
