using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zebra.ProductService.Domain.Views;
using Zebra.ProductService.Persistance.Context;

namespace Zebra.ProductService.Persistance.Repository.ProductPriceView
{
    public class ProductPriceRepo : IProductPriceRepo
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductPriceRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProductPriceModel>> GetProductPrices() =>
            await _dbContext.ProductPriceView.ToListAsync();
    }
}
