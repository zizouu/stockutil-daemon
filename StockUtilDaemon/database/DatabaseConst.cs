using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockUtilDaemon
{
    class DatabaseConst
    {
        public static String DATABASE_IP = "loacalhost";
        public static String DATABASE_NAME = "stock_test";
        public static String DATABASE_ID = "postgres";
        public static String DATABASE_PW = "h382612";

        public static String SELECT_ALL_DAILY_TRADING = "select * from daily_trading";
    }
}
