using System;
using System.ComponentModel.DataAnnotations.Schema;
using Zebra.ProductService.Domain.Shared;

namespace Zebra.ProductService.Domain.Entities
{
    public class RatingModel : Entity
    {
        public string Review { get; set; }
        public int Score { get; set; }
        public DateTime AddDate { get; set; }

        [ForeignKey("ProductModelId")]
        public Guid ProductId { get; set; }
    }
}
