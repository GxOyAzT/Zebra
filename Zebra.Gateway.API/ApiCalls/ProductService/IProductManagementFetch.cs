using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zebra.ProductService.Domain.Entities;

namespace Zebra.Gateway.API.ApiCalls.ProductService
{
    public interface IProductManagementFetch
    {
        [Get("/api/productmanagement/getproducts")]
        Task<List<ProductModel>> GetProducts();
    }
}
