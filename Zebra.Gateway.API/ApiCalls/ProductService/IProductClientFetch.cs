using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zebra.Gateway.API.ApiModels.ProductService;

namespace Zebra.Gateway.API.ApiCalls.ProductService
{
    public interface IProductClientFetch
    {
        [Get("/api/productclient/getallavaliableproducts")]
        Task<List<ProductPriceApiModel>> GetAllAvaliableProducts([Header("Accept-Language")] string lang);
    }
}
