using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using Zebra.Shared.LoggerDriver.Domain.Enums;
using Zebra.Shared.LoggerDriver.Domain.Models;
using Zebra.Shared.LoggerDriver.RabbitMqConfiguration;
using Zebra.Shared.LoggerDriver.RabbitMqConfiguration.Interfaces;
using Zebra.Shared.LoggerDriver.Services.Interfaces;

namespace Zebra.Shared.LoggerDriver.Services
{
    public class MessageLogger : IMessageLogger
    {
        private readonly ICreateModel _createModel;
        private readonly Settings _settings;

        public MessageLogger(
            ICreateModel createModel,
            Settings settings)
        {
            _createModel = createModel;
            _settings = settings;
        }

        public void Log(string message, LogTypeEnum logType)
        {
            if (!_settings._IsProduction)
            {
                return;
            }

            var logMessage = new LogModel()
            {
                Message = message,
                LogType = logType,
                Sender = _settings._SenderName
            };

            var model = _createModel.Create();

            model.BasicPublish(exchange: "Logger", routingKey: "", basicProperties: null, body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(logMessage)));
        }
    }
}
