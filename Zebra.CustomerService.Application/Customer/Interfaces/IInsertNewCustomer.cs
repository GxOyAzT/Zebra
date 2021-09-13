using Zebra.CustomerService.Application.Shared.ErrorCollector;
using Zebra.CustomerService.Domain.Models.ApiModels;

namespace Zebra.CustomerService.Application.Customer.Interfaces
{
    public interface IInsertNewCustomer : IErrorCollector
    {
        void Execute(CustomerApiModel customerModel);
    }
}
