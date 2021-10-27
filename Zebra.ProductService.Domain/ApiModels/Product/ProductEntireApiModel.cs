using System;
using System.Collections.Generic;
using System.Linq;
using Zebra.ProductService.Domain.Entities;

namespace Zebra.ProductService.Domain.ApiModels.Product
{
    public class ProductEntireApiModel
    {
        public ProductModel Product { get; set; }

        public ICollection<PriceModel> Prices { private get; set; }
        public ICollection<PriceModel> PricePremieres 
        { 
            get 
            {
                if (Prices is null)
                {
                    return null;
                }

                return Prices.Where(e => e.From > DateTime.Today).ToList();
            } 
        }
        public ICollection<PriceModel> HistoryPrices
        {
            get
            {
                if (Prices is null)
                {
                    return null;
                }

                var pricesOlderThenToday = Prices.Where(e => e.From < DateTime.Today).OrderByDescending(e => e.From).ToList();

                if (pricesOlderThenToday.Count == 1)
                {
                    return new List<PriceModel>();
                }

                pricesOlderThenToday.Remove(pricesOlderThenToday.FirstOrDefault());

                return pricesOlderThenToday;
            }
        }
        public PriceModel ActualPrice
        {
            get
            {
                if (Prices is null)
                {
                    return null;
                }

                var actualPrice = Prices.Where(e => e.From <= DateTime.Today).OrderByDescending(e => e.From).FirstOrDefault();

                if (actualPrice is null)
                {
                    return null;
                }

                return actualPrice;
            }
        }

        public ICollection<RatingModel> Ratings { get; set; }
        public float AverageRating 
        {
            get
            {
                if (Ratings is null)
                {
                    return 0;
                }

                if (Ratings.Count == 0)
                {
                    return 0;
                }

                return (float)Math.Round(Ratings.Sum(e => e.Score) * 1.0 / Ratings.Count, 1);
            }
        }

        public string ImageSrc { get; set; }

        public string ReadableAddProductDate
        {
            get => Product != null ? Product.AddDate.ToString("dd-MM-yyyy") : "";
        }
    }
}
