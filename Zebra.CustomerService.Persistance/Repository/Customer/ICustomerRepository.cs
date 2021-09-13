using Zebra.CustomerService.Domain.Models.Tables;
using Zebra.CustomerService.Persistance.Repository.Shared;

namespace Zebra.CustomerService.Persistance.Repository.Customer
{
    public interface ICustomerRepository : IRepository<CustomerModel>
    {
    }
}
