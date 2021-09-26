using System;

namespace Zebra.Gateway.API.ApiModels.ProductService
{
    public class ProductPriceApiModel
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public decimal Cost { get; set; }
    }
}
