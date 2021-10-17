using RabbitMQ.Client;

namespace Zebra.AuthService.API.Services.RabbitModel
{
    public interface ICreateModel
    {
        IModel Create();
    }
}
