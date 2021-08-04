using RabbitMQ.Client;

namespace Zebra.Shared.LoggerDriver.RabbitMqConfiguration.Interfaces
{
    public interface ICreateModel
    {
        IModel Create();
    }
}
