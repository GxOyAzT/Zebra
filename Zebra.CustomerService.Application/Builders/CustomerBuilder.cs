using System;
using Zebra.CustomerService.Domain.Enums;
using Zebra.CustomerService.Domain.Models;
using Zebra.CustomerService.Domain.ValueObjects;

namespace Zebra.CustomerService.Application.Builders
{
    public class CustomerBuilder
    {
        protected CustomerModel Customer { get; set; }

        public CustomerBuilder()
        {
            Customer = new CustomerModel();
            Customer.Address = new Address();
            Customer.Points = 0;
        }

        protected CustomerBuilder(CustomerModel customer)
        {
            Customer = customer;
        }

        public AddressBuilder Address => new AddressBuilder(Customer);

        public CustomerBuilder AddFullName(string name)
        {
            Customer.FullName = name;
            return this;
        }

        public CustomerBuilder AddDob(DateTime dob)
        {
            Customer.Dob = dob;
            return this;
        }

        public CustomerBuilder AddGender(GenderEnum gender)
        {
            Customer.Gender = gender;
            return this;
        }
    }
}
