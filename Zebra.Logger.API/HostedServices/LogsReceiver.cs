using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zebra.Logger.API.Domain.Models;
using Zebra.Logger.API.Persistance.Interfaces;
using Zebra.Logger.API.RabbitMqConfiguration.Interfaces;

namespace Zebra.Logger.API.HostedServices
{
    public class LogsReceiver : BackgroundService
    {
        private readonly IModel _channel;
        private readonly ILogger<LogsReceiver> _logger;
        private readonly ILogsRepository _logsRepository;

        public LogsReceiver(
            ICreateModel createModel, 
            ILogger<LogsReceiver> logger,
            ILogsRepository logsRepository)
        {
            _channel = createModel.Create();
            _logger = logger;
            _logsRepository = logsRepository;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var logMessage = JsonConvert.DeserializeObject<LogModel>(content);

                _logger.Log(LogLevel.Information, $"Message: {logMessage.Message} LogType: {logMessage.LogType} Time: {logMessage.Time} Sender: {logMessage.Sender}");

                _logsRepository.Insert(logMessage);

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume("InsertLogQueue", false, consumer);

            return Task.CompletedTask;
        }
    }
}
