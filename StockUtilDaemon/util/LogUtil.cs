using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockUtilDaemon
{
    class LogUtil
    {
        private static String BIG_LINE = "========================================";
        public static void logConsole(String s)
        {
            Console.WriteLine(BIG_LINE + s + BIG_LINE);
        }
    }
}
