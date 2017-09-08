using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Npgsql;
using System.Threading;
using System.Threading.Tasks;

namespace StockUtilDaemon
{
    class DatabaseController
    {
        private String DATABASE_CONNECTION_INFO = "Server=" + DatabaseConst.DATABASE_IP + ";"
                                               + "Database=" + DatabaseConst.DATABASE_NAME + ";"
                                               + "User ID=" + DatabaseConst.DATABASE_ID + ";"
                                               + "Password=" + DatabaseConst.DATABASE_PW + ";";
        private NpgsqlConnection connection;
        //private NpgsqlCommand command;

        public DatabaseController()
        {
            initConnection();
        }

        public void initConnection()
        {
            try
            {
                LogUtil.logConsole("DB Connection Start");
                this.connection = new NpgsqlConnection(DATABASE_CONNECTION_INFO);
                this.connection.Open();
                //this.command = connection.CreateCommand();
                LogUtil.logConsole("DB Connection Finish");
            }
            catch(Exception e)
            {
                LogUtil.logConsole(e.Message);
            }
            
        }

        public void insertDailyBatchDataForTest(List<DailyTradingModel> list)
        {
            //TODO: Insert Logic
            DailyTradingModel model = list.ElementAt(0);
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = DatabaseConst.INSERT_DAILY_TRADING;
            command.Parameters.Add("code", NpgsqlTypes.NpgsqlDbType.Varchar).Value = model.Code;
            command.Parameters.Add("name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = model.Name;
            command.Parameters.Add("startPrice", NpgsqlTypes.NpgsqlDbType.Integer).Value = model.StartPrice;
            command.Parameters.Add("endPrice", NpgsqlTypes.NpgsqlDbType.Integer).Value = model.EndPrice;
            command.Parameters.Add("highPrice", NpgsqlTypes.NpgsqlDbType.Integer).Value = model.HighPrice;
            command.Parameters.Add("lowPrice", NpgsqlTypes.NpgsqlDbType.Integer).Value = model.LowPrice;
            command.Parameters.Add("volume", NpgsqlTypes.NpgsqlDbType.Integer).Value = model.Volume;
            command.Parameters.Add("tradingValue", NpgsqlTypes.NpgsqlDbType.Integer).Value = model.TradingValue;
            command.Parameters.Add("tradingDate", NpgsqlTypes.NpgsqlDbType.Date).Value = model.TradingDate;
            command.Parameters.Add("createdDate", NpgsqlTypes.NpgsqlDbType.Date).Value = model.CreatedDate;
            command.Parameters.Add("modifiedDate", NpgsqlTypes.NpgsqlDbType.Date).Value = model.ModifiedDate;
            command.ExecuteNonQuery();
        }

        public int insertDailyBatchData(List<DailyTradingModel> list)
        {
            int result = 0;
            Stopwatch watch = new Stopwatch();
            watch.Start();
            ThreadPoolChecker checker = new ThreadPoolChecker(list.Count);

            Parallel.For(0, list.Count, i =>
            {
                using (NpgsqlConnection connectionParallel = new NpgsqlConnection(this.DATABASE_CONNECTION_INFO))
                {
                    connectionParallel.Open();
                    using(NpgsqlCommand commandParallel = new NpgsqlCommand(DatabaseConst.INSERT_DAILY_TRADING, connectionParallel))
                    {
                        DailyTradingModel model = list.ElementAt<DailyTradingModel>(i);
                        commandParallel.Parameters.Add("code", NpgsqlTypes.NpgsqlDbType.Varchar).Value = model.Code;
                        commandParallel.Parameters.Add("name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = model.Name;
                        commandParallel.Parameters.Add("startPrice", NpgsqlTypes.NpgsqlDbType.Integer).Value = model.StartPrice;
                        commandParallel.Parameters.Add("endPrice", NpgsqlTypes.NpgsqlDbType.Integer).Value = model.EndPrice;
                        commandParallel.Parameters.Add("highPrice", NpgsqlTypes.NpgsqlDbType.Integer).Value = model.HighPrice;
                        commandParallel.Parameters.Add("lowPrice", NpgsqlTypes.NpgsqlDbType.Integer).Value = model.LowPrice;
                        commandParallel.Parameters.Add("volume", NpgsqlTypes.NpgsqlDbType.Integer).Value = model.Volume;
                        commandParallel.Parameters.Add("tradingValue", NpgsqlTypes.NpgsqlDbType.Integer).Value = model.TradingValue;
                        commandParallel.Parameters.Add("tradingDate", NpgsqlTypes.NpgsqlDbType.Date).Value = model.TradingDate;
                        commandParallel.Parameters.Add("createdDate", NpgsqlTypes.NpgsqlDbType.Date).Value = model.CreatedDate;
                        commandParallel.Parameters.Add("modifiedDate", NpgsqlTypes.NpgsqlDbType.Date).Value = model.ModifiedDate;
                        try
                        {
                            commandParallel.ExecuteScalar();
                        }
                        catch(Exception e)
                        {
                            LogUtil.logConsole(e.Message);
                        }
                        
                    }
                }
            });

            watch.Stop();
            LogUtil.logConsole(" insert " + result + " items take " + watch.ElapsedMilliseconds / 1000 + "seconds");
            return result;
        }

        public List<DailyTradingModel> selectAllStockTrading()
        {
            List<DailyTradingModel> modelList = new List<DailyTradingModel>();
            NpgsqlCommand command = connection.CreateCommand();
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
            reader.Close();
            return modelList;
        }

        public Dictionary<String, String> selectAllStockItem()
        {
            Dictionary<String, String> itemDic = new Dictionary<String, String>();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = DatabaseConst.SELECT_ALL_STOCK_ITEM;
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                itemDic.Add((String)reader["code"], (String)reader["name"]);
            }
            reader.Close();
            return itemDic;
        }

        public String selectLastUpdateDate()
        {
            String lastUpdateDate;
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = DatabaseConst.SELECT_LAST_UPDATE_DATE;
            NpgsqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                lastUpdateDate = (String)reader["value"];
            }
            else
            {
                lastUpdateDate = "FIRST";
            }
            reader.Close();
            return lastUpdateDate;
        }

        public int updateLastUpdateDate()
        {
            int count = 0;
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = DatabaseConst.UPDATE_LAST_UPDATE_DATE;
            command.Parameters.Add("value", NpgsqlTypes.NpgsqlDbType.Varchar).Value = DateUtil.convertDateTimeToString(DateTime.Now);
            count = command.ExecuteNonQuery();

            return count;
        }

        public int insertLastUpdateDate()
        {
            int count = 0;
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = DatabaseConst.INSERT_LAST_UPDATE_DATE;
            command.Parameters.Add("configName", NpgsqlTypes.NpgsqlDbType.Varchar).Value = DatabaseConst.CONFIG_NAME_LAST_UPDATE;
            command.Parameters.Add("value", NpgsqlTypes.NpgsqlDbType.Varchar).Value = DateUtil.convertDateTimeToString(DateTime.Now);
            count = command.ExecuteNonQuery();

            return count;
        }

        public int insertStockItem(StockItemModel item)
        {
            int count = 0;
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = DatabaseConst.INSERT_STOCK_ITEM;
            command.Parameters.Add("code", NpgsqlTypes.NpgsqlDbType.Varchar).Value = item.Code;
            command.Parameters.Add("name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = item.Name;
            count = command.ExecuteNonQuery();
            return count;
        }

        public DateTime selectRecentTradingDate(String code)
        {
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = DatabaseConst.SELECT_RECENT_TRADING_DATE;
            command.Parameters.Add("code", NpgsqlTypes.NpgsqlDbType.Varchar).Value = code;
            NpgsqlDataReader reader = command.ExecuteReader();

            // if not exist trading_date then return 1/1/1
            DateTime recentTradingDate = new DateTime(1, 1, 1);

            while (reader.Read())
            {
                recentTradingDate = (DateTime)reader["trading_date"];
             }
            reader.Close();

            return recentTradingDate;
        }
    }
}
