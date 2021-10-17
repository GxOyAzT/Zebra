using RabbitMQ.Client;
using System;

namespace Zebra.AuthService.API.Services.RabbitModel
{
    public class CreateModel : ICreateModel
    {
        public IModel Create()
        {
            var connectionFactory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };

            var connection = connectionFactory.CreateConnection();

            var model = connection.CreateModel();

            return model;
        }
    }
}
