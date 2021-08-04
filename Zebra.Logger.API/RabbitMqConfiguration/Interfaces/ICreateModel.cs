using RabbitMQ.Client;

namespace Zebra.Logger.API.RabbitMqConfiguration.Interfaces
{
    public interface ICreateModel
    {
        IModel Create();
    }
}
