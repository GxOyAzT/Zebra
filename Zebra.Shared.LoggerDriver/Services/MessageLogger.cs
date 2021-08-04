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
        private readonly SenderName _senderName;

        public MessageLogger(
            ICreateModel createModel,
            SenderName senderName)
        {
            _createModel = createModel;
            _senderName = senderName;
        }

        public void Log(string message, LogTypeEnum logType)
        {
            var logMessage = new LogModel()
            {
                Message = message,
                LogType = logType,
                Sender = _senderName._SenderName
            };

            var model = _createModel.Create();

            model.BasicPublish(exchange: "Logger", routingKey: "", basicProperties: null, body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(logMessage)));
        }
    }
}
