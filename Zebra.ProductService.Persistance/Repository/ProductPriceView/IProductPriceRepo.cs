using System.Collections.Generic;
using System.Threading.Tasks;
using Zebra.ProductService.Domain.Views;

namespace Zebra.ProductService.Persistance.Repository.ProductPriceView
{
    public interface IProductPriceRepo
    {
        Task<List<ProductPriceModel>> GetProductPrices();
    }
}
