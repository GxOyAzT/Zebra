using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Persistance.Context;
using Zebra.ProductService.Persistance.Repository.Shared;

namespace Zebra.ProductService.Persistance.Repository.Rating
{
    public class RatingRepository : Repository<RatingModel>, IRatingRepository
    {
        public RatingRepository(ApplicationDbContext dbContext)
            :base(dbContext)
        {
        }
    }
}
