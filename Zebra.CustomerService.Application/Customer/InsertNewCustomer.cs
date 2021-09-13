using Zebra.CustomerService.Application.Builders;
using Zebra.CustomerService.Application.Customer.Interfaces;
using Zebra.CustomerService.Application.Shared.ErrorCollector;
using Zebra.CustomerService.Application.Validation.Customer;
using Zebra.CustomerService.Domain.Models.ApiModels;
using Zebra.CustomerService.Persistance.Repository.Customer;

namespace Zebra.CustomerService.Application.Customer
{
    public class InsertNewCustomer : ErrorCollector
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerValidator _customerValidator;

        public InsertNewCustomer(
            ICustomerRepository customerRepository,
            ICustomerValidator customerValidator)
        {
            _customerRepository = customerRepository;
            _customerValidator = customerValidator;
        }

        public void Execute(CustomerApiModel customerModel)
        {
            if (!_customerValidator.IsValid(customerModel))
            {
                _customerValidator.GetErrors().ForEach(e => this.AddError(e));
                return;
            }

            var insertCustomer = new CustomerNewBuilder()
                .AddDob(customerModel.Dob)
                .AddFullName(customerModel.FullName)
                .AddGender(customerModel.Gender)
                .Address
                    .City(customerModel.City)
                    .Street(customerModel.Street);

            _customerRepository.Insert(insertCustomer);

            return;
        }
    }
}
