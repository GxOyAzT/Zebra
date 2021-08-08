using System;
using Zebra.ProductService.Domain.Shared;

namespace Zebra.ProductService.Domain.Entities
{
    public class RatingModel : Entity
    {
        public Guid ProductId { get; set; }
        public string Review { get; set; }
        public int Score { get; set; }
        public DateTime AddDate { get; set; }
    }
}
