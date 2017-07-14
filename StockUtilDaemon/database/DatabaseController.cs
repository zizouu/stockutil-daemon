using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace StockUtilDaemon
{
    class DatabaseController
    {
        private String DATABASE_CONNECTION_INFO = "Server=" + DatabaseConst.DATABASE_IP + ";"
                                               + "Database=" + DatabaseConst.DATABASE_NAME + ";"
                                               + "User ID=" + DatabaseConst.DATABASE_ID + ";"
                                               + "Password=" + DatabaseConst.DATABASE_PW + ";";
        private NpgsqlConnection connection;
        private NpgsqlCommand command;

        public DatabaseController()
        {
            initConnection();
        }

        public void initConnection()
        {
            this.connection = new NpgsqlConnection(DATABASE_CONNECTION_INFO);
            this.connection.Open();
            this.command = connection.CreateCommand();
        }

        public void insertDailyBatchData(List<DailyTradingModel> list)
        {
            foreach(DailyTradingModel model in list){
                //TODO: Insert Logic
                //String.Format("insert into daily_trading")
            }
        }

        public void selectAllStockTrading()
        {
            List<DailyTradingModel> modelList = new List<DailyTradingModel>();
            command.CommandText = DatabaseConst.SELECT_ALL_DAILY_TRADING;
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                DailyTradingModel model = new DailyTradingModel((String) reader["code"], 
                                                                (String) reader["name"],
                                                                (int) reader["start_price"],
                                                                (int) reader["end_price"],
                                                                (int) reader["high_price"],
                                                                (int) reader["low_price"],
                                                                (int) reader["volume"],
                                                                (int) reader["trading_value"],
                                                                DateUtil.convertStringToDateTime((String) reader["trading_date"]),
                                                                DateUtil.convertStringToDateTime((String) reader["created_date"]),
                                                                DateUtil.convertStringToDateTime((String) reader["modified_date"])
                                                                );
                modelList.Add(model);
            }
        }
    }
}
