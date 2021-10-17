using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zebra.AuthService.API.Models;
using Zebra.AuthService.API.Services.RabbitModel;
using Zebra.Shared.LoggerDriver.Domain.Enums;
using Zebra.Shared.LoggerDriver.Services.Interfaces;

namespace Zebra.AuthService.API.HostedServices
{
    public class NewCustomerReceiver : BackgroundService
    {
        private readonly IModel _channel;
        private readonly ILogger<NewCustomerReceiver> _logger;
        private readonly IServiceProvider _services;

        public NewCustomerReceiver(
            ICreateModel createModel,
            ILogger<NewCustomerReceiver> logger,
            IServiceProvider services)
        {
            _channel = createModel.Create();
            _logger = logger;
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var newCustomerEvMod = JsonConvert.DeserializeObject<CustomerCreateEventModel>(content);

                _logger.Log(LogLevel.Information, $"CustomerId: {newCustomerEvMod.CustomerId} UserId: {newCustomerEvMod.UserId}");

                using (var scope = _services.CreateScope())
                {
                    var messageLogger = scope.ServiceProvider.GetRequiredService<IMessageLogger>();
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                    var user = await userManager.FindByIdAsync(newCustomerEvMod.UserId);

                    if (user != null)
                    {
                        user.IsCustomerCreated = true;
                        user.CustomerId = newCustomerEvMod.CustomerId;
                        await userManager.UpdateAsync(user);
                    }
                    if (user == null)
                    {
                        messageLogger.Log($"Cannot update customer info of user. user ID {newCustomerEvMod.UserId} not exists. (AuthAPI.NewCustomerReceiver.ExecuteAsync)", LogTypeEnum.Error);
                    }
                }

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume("CustomerCreateLog", false, consumer);
        }
    }
}
