using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Persistance.Context;
using Zebra.ProductService.Persistance.Repository.Shared;

namespace Zebra.ProductService.Persistance.Repository.Price
{
    public class PriceRepository : Repository<PriceModel>, IPriceRepository
    {
        public PriceRepository(ApplicationDbContext dbContext)
            :base (dbContext)
        {
        }
    }
}
