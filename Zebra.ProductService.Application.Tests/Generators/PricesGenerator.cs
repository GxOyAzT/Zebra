using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Zebra.ProductService.Persistance.Context;

namespace Zebra.ProductService.Application.Tests.Generators
{
    public class PricesGenerator
    {
        [Fact]
        public void Generate()
        {
            using (var db = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Zebra_Product;").Options))
            {
                for (int i = 0; i < 30; i++)
                {
                    db.Prices.Add(
                    new Domain.Entities.PriceModel()
                    {
                        ProductId = Guid.Parse("1E5393D3-3524-47D2-B304-08D99AB6A5C7"),
                        Cost = new Random().Next(100, 20000) / 100,
                        Tax = 18,
                        From = DateTime.Now.AddDays(-300).AddDays(new Random().Next(1, 600))
                    });
                }

                db.SaveChanges();
            }
        }
    }
}
