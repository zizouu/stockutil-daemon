using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockUtilDaemon
{
    class DataUtil
    {
        public List<String> parseCodeData(String codeData)
        {
            String[] codeArray = codeData.Split(';');
            List<String> codeList = new List<String>(codeArray);

            return codeList;
        }
    }
}
