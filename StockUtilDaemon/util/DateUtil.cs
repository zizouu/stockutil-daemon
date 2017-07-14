using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockUtilDaemon
{
    class DateUtil
    {
        public static DateTime convertStringToDateTime(String dateString)
        {
            String[] dateArray = new String[3];

            if (dateString.Length == 8)
            {
                dateArray = parseYearMonthDate(dateString);
            }
            return new DateTime(int.Parse(dateArray[0]), int.Parse(dateArray[1]), int.Parse(dateArray[2]));
        }

        public static String[] parseYearMonthDate(String dateString)
        {
            String[] dateArray = new String[3];
            dateArray[0] = dateString.Substring(0, 4);
            dateArray[1] = dateString.Substring(4, 2);
            dateArray[2] = dateString.Substring(6, 2);

            return dateArray;
        }
    }
}
