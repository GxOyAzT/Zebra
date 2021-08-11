using Zebra.CustomerService.Domain.Models;

namespace Zebra.CustomerService.Application.Customer.Interfaces
{
    public interface IInsertNewCustomer
    {
        void Execute(CustomerModel customerModel);
    }
}
