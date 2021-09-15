using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zebra.ProductService.Domain.Entities;

namespace Zebra.ProductService.Application.ApiModels
{
    public class ProductPriceApiModel
    {
        public string ProductName { get; set; }
        public decimal Cost { get; set; }
    }
}
