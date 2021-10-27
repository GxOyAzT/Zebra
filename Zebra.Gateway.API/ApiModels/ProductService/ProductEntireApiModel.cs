using System;
using System.Collections.Generic;

namespace Zebra.Gateway.API.ApiModels.ProductService
{
    public class ProductEntireApiModel
    {
        public ProductModel Product { get; set; }

        public ICollection<PriceModel> PricePremieres { get; set; }
        public ICollection<PriceModel> HistoryPrices { get; set; }
        public PriceModel ActualPrice { get; set; }

        public ICollection<RatingModel> Ratings { get; set; }
        public float AverageRating { get; set; }

        public string ImageSrc { get; set; }

        public string ReadableAddProductDate { get; set; }
    }
}
