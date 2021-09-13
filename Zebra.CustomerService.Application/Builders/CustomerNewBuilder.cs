using System;
using Zebra.CustomerService.Domain.Enums;
using Zebra.CustomerService.Domain.Models;
using Zebra.CustomerService.Domain.Models.Tables;
using Zebra.CustomerService.Domain.ValueObjects;

namespace Zebra.CustomerService.Application.Builders
{
    public class CustomerNewBuilder
    {
        protected CustomerModel Customer { get; set; }

        public CustomerNewBuilder()
        {
            Customer = new CustomerModel();
            Customer.Address = new Address();
            Customer.Points = 0;
        }

        protected CustomerNewBuilder(CustomerModel customer)
        {
            Customer = customer;
        }

        public AddressBuilder Address => new AddressBuilder(Customer);

        public CustomerNewBuilder AddFullName(string name)
        {
            Customer.FullName = name;
            return this;
        }

        public CustomerNewBuilder AddDob(DateTime dob)
        {
            Customer.Dob = dob;
            return this;
        }

        public CustomerNewBuilder AddGender(GenderEnum gender)
        {
            Customer.Gender = gender;
            return this;
        }

        public static implicit operator CustomerModel(CustomerNewBuilder builder)
        {
            return builder.Customer;
        }
    }
}
