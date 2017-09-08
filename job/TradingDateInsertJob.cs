using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Npgsql;

namespace StockUtilDaemon
{
    class TradingDateInsertJob
    {
        private NpgsqlConnection connection;
        private DailyTradingModel model;

        public TradingDateInsertJob(NpgsqlConnection connection, DailyTradingModel model)
        {
            this.connection = connection;
            this.model = model;
        }

        public void threadPoolCallback(object threadContext)
        {
            int result = insertDailyTradingModel();
        }

        public int insertDailyTradingModel()
        {
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
            return command.ExecuteNonQuery();
        }
    }
}
