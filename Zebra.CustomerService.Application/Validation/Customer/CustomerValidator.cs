using System;
using Zebra.CustomerService.Application.Shared.ErrorCollector;
using Zebra.CustomerService.Domain.Models;

namespace Zebra.CustomerService.Application.Validation.Customer
{
    public class CustomerValidator : ErrorCollector, ICustomerValidator
    {
        public bool IsValid(CustomerModel customerModel)
        {
            throw new NotImplementedException();
        }
    }
}
