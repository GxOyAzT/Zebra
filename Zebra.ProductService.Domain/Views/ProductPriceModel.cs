using System;

namespace Zebra.ProductService.Domain.Views
{
    public class ProductPriceModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public decimal Cost { get; set; }
    }
}
