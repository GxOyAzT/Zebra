using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using Zebra.Logger.API.Domain.Models;
using Zebra.Logger.API.Persistance.Interfaces;

namespace Zebra.Logger.API.Persistance
{
    public class LogsRepository : ILogsRepository
    {
        private readonly IConfiguration _configuration;

        public LogsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Insert(LogModel logModel)
        {
            logModel.Id = Guid.NewGuid();

            using (var connection = new SqliteConnection($"Data Source={_configuration["SQLiteFileName"]}"))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = @$"INSERT INTO LogModels (Id, LogType, Sender, Time, Message) VALUES ('{logModel.Id}', {(int)logModel.LogType}, '{logModel.Sender}', '{logModel.Time}', '{logModel.Message}')";

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
