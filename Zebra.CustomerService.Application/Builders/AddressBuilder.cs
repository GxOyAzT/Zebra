using Zebra.CustomerService.Domain.Models;

namespace Zebra.CustomerService.Application.Builders
{
    public class AddressBuilder : CustomerBuilder
    {
        public AddressBuilder(CustomerModel customerModel)
            :base(customerModel)
        {
            base.Customer = customerModel;
        }

        public AddressBuilder Street(string street)
        {
            Customer.Address.Street = street;
            return this;
        }

        public AddressBuilder City(string city)
        {
            Customer.Address.City = city;
            return this;
        }
    }
}
