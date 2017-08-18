using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockUtilDaemon
{
    class StockItemModel
    {
        private String code;
        private String name;

        public StockItemModel(String code, String name)
        {
            this.code = code;
            this.name = name;
        }

        public String Code { get { return code; } }

        public String Name { get { return name; } }
    }
}
