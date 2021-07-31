using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Persistance.Repository.Shared;

namespace Zebra.ProductService.Persistance.Repository.Product
{
    public interface IProductRepository : IRepository<ProductModel>
    {
    }
}
