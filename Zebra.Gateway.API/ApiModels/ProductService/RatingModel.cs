using System;

namespace Zebra.Gateway.API.ApiModels.ProductService
{
    public class RatingModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Review { get; set; }
        public int Score { get; set; }
        public DateTime AddDate { get; set; }
    }
}
