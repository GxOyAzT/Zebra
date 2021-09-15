using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zebra.Gateway.API.ApiCalls.ProductService.Queries;
using Zebra.ProductService.Domain.Entities;

namespace Zebra.Gateway.API.ApiCalls.ProductService
{
    public interface IProductManagementFetch
    {
        [Get("/api/productmanagement/getproducts")]
        Task<List<ProductModel>> GetProducts();

        [Get("/api/productmanagement/getproduct")]
        Task<ProductModel> GetProduct([Body] GetProductQuery query);
    }
}
