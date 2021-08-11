using Zebra.CustomerService.Application.Customer.Interfaces;
using Zebra.CustomerService.Application.Shared.ErrorCollector;
using Zebra.CustomerService.Domain.Models;
using Zebra.CustomerService.Persistance.Repository.Customer;

namespace Zebra.CustomerService.Application.Customer
{
    public class InsertNewCustomer : ErrorCollector, IInsertNewCustomer
    {
        private readonly ICustomerRepository _customerRepository;

        public InsertNewCustomer(
            ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void Execute(CustomerModel customerModel)
        {
            
        }
    }
}
