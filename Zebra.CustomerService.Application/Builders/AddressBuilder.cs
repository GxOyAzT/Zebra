using Zebra.CustomerService.Domain.Models.Tables;

namespace Zebra.CustomerService.Application.Builders
{
    public class AddressBuilder : CustomerNewBuilder
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
