using System;
using System.Collections.Generic;

namespace Zebra.Gateway.API.ApiModels.ProductService
{
    public class ProductModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsInSale { get; set; }
        public DateTime AddDate { get; set; }
        public string ImagePath { get; set; }
        public string Ean { get; set; }

        public ICollection<PriceModel> Prices { get; set; }
        public ICollection<RatingModel> Ratings { get; set; }
    }
}
