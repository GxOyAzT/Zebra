using System;

namespace Zebra.OrderService.Domain.Models
{
    public class ProductModel
    {
        public Guid ProductId { get; set; }
        public decimal RetailPrice { get; set; }
        public int Quantity { get; set; }
    }
}
