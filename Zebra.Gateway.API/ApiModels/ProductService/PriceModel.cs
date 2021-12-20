using System;

namespace Zebra.Gateway.API.ApiModels.ProductService
{
    public class PriceModel
    {
        public Guid Id { get; set; }

        public int Tax { get; set; }
        public decimal Cost { get; set; }
        public DateTime From { get; set; }

        public Guid ProductId { get; set; }
    }
}
