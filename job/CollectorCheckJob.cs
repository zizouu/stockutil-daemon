using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockUtilDaemon
{
    class CollectorCheckJob
    {
        private Login parent;
        private StockDataCollector collector;

        public CollectorCheckJob(Login parent, StockDataCollector collector)
        {
            this.parent = parent;
            this.collector = collector;
        }

        public void checkCollectorStatus()
        {
            String currentStatus;

            do
            {
                currentStatus = collector.getCollectorStatus();
                checkAndSetStatusBox(currentStatus);
            } while (!StockDataCollector.STATUS_FINISH.Equals(currentStatus));
        }

        private void checkAndSetStatusBox(String status)
        {
            if (!status.Equals(parent.getCollectorStatus()))
            {
                parent.setCollectorStatus(status);
            }
        }
    }
}
