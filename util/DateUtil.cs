﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace StockUtilDaemon
{
    class DateUtil
    {
        public static String trimWhiteSpace(String src)
        {
            src = Regex.Replace(src, " ", "");
            return src;
        }

        public static String replaceString(String src, String from, String to)
        {
            src = Regex.Replace(src, from, to);
            return src;
        }

        public static String convertDateTimeToString(DateTime date)
        {
            String dateString = date.ToShortDateString();
            dateString = replaceString(dateString, "-", "");
               
            return dateString;
        }

        public static DateTime convertStringToDateTime(String dateString)
        {
            String[] dateArray = new String[3];

            dateString = trimWhiteSpace(dateString);
            dateString = replaceString(dateString, "-", "");

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

        public static List<DateTime> getPeriodDateTimeExceptWeekend(DateTime fromDate, int period)
        {
            List<DateTime> listDateTime = new List<DateTime>();
            for(int i = 0; i <= period; i++)
            {
                DateTime date = fromDate.AddDays(i);
                if (isNotWeekend(date))
                {
                    listDateTime.Add(date);
                }
            }
            return listDateTime;
        }

        public static Boolean isNotWeekend(DateTime date)
        {
            String day = date.ToString("ddd");
            if ("Sat".Equals(day) || "Sun".Equals(day)){
                return false;
            }
            return true;
        }
    }
}
