using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Persistance.Context;
using Zebra.ProductService.Persistance.Repository.Shared;

namespace Zebra.ProductService.Persistance.Repository.Product
{
    public class ProductRepository : Repository<ProductModel>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext)
            :base(dbContext)
        {
        }
    }
}
