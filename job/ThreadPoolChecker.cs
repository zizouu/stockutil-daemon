using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockUtilDaemon
{
    class ThreadPoolChecker
    {
        private bool[] isSetArray;

        public ThreadPoolChecker(int size)
        {
            this.isSetArray = new bool[size];
        }

        public bool isComplete()
        {
            foreach(bool isSet in isSetArray)
            {
                if (!isSet)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
