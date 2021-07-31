using System;
using Zebra.ProductService.Domain.Shared;

namespace Zebra.ProductService.Domain.Entities
{
    public class PriceModel : Entity
    {
        public int Tax { get; set; }
        public decimal Cost { get; set; }

        public Guid ProductModelId { get; set; }
    }
}
