using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zebra.ProductService.Application.ApiModels;

namespace Zebra.Gateway.API.ApiCalls.ProductService
{
    public interface IProductClientFetch
    {
        [Get("/api/productclient/getallavaliableproducts")]
        Task<List<ProductPriceApiModel>> GetAllAvaliableProducts();
    }
}
