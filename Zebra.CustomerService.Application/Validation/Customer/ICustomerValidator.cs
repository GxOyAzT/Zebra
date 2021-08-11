using Zebra.CustomerService.Application.Shared.ErrorCollector;
using Zebra.CustomerService.Domain.Models;

namespace Zebra.CustomerService.Application.Validation.Customer
{
    public interface ICustomerValidator : IErrorCollector
    {
        public bool IsValid(CustomerModel customerModel);
    }
}
