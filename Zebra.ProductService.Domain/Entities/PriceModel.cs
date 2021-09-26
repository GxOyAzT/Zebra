using System;
using System.ComponentModel.DataAnnotations.Schema;
using Zebra.ProductService.Domain.Shared;

namespace Zebra.ProductService.Domain.Entities
{
    public class PriceModel : Entity
    {
        public int Tax { get; set; }
        public decimal Cost { get; set; }
        public DateTime From { get; set; }

        [ForeignKey("ProductModelId")]
        public Guid ProductId { get; set; }
    }
}
