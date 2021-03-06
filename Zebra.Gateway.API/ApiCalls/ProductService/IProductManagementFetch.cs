using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zebra.Gateway.API.ApiCalls.ProductService.Commands;
using Zebra.Gateway.API.ApiModels.ProductService;

namespace Zebra.Gateway.API.ApiCalls.ProductService
{
    public interface IProductManagementFetch
    {
        [Get("/api/productmanagement/getproducts")]
        Task<List<ProductModel>> GetProducts([Header("Accept-Language")] string lang);

        [Get("/api/productmanagement/getproduct/{productId}")]
        Task<ProductModel> GetProduct([AliasAs("productId")] Guid productId, [Header("Accept-Language")] string lang);

        [Get("/api/productmanagement/getentireproduct/{productId}")]
        Task<ProductEntireApiModel> GetEntireProduct(Guid productId, string lang);

        [Put("/api/productmanagement/updateproduct")]
        Task UpdateProduct([Body] UpdateProductCommand command, [Header("Accept-Language")] string lang);

        [Post("/api/productmanagement/addproduct")]
        Task AddProduct([Body] AddProductCommand command, [Header("Accept-Language")] string lang);
    }
}
