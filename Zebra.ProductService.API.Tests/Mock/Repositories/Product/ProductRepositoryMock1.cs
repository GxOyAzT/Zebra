using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Persistance.Repository.Product;

namespace Zebra.ProductService.API.Tests.Mock.Repositories.Product
{
    public class ProductRepositoryMock1 : IProductRepository
    {
        public List<ProductModel> ProductModels { get; set; }

        public ProductRepositoryMock1()
        {
            ProductModels = new List<ProductModel>()
            {
                new ProductModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    IsInSale = true,
                    AddDate = DateTime.Now.AddDays(-10),
                    Name = "Product 1",
                    Description = "Description 1",
                },
                new ProductModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    IsInSale = true,
                    AddDate = DateTime.Now.AddDays(-20),
                    Name = "Product 2",
                    Description = "Description 2",
                },
                new ProductModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    IsInSale = true,
                    AddDate = DateTime.Now.AddDays(-100),
                    Name = "Product 3",
                    Description = "Description 3",
                },
                new ProductModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    IsInSale = true,
                    AddDate = DateTime.Now.AddDays(-10),
                    Name = "Product 4",
                    Description = "Description 4",
                }
            };
        }

        public Task Delete(ProductModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<ProductModel> Get(Guid id)
        {
            return Task.FromResult(ProductModels.FirstOrDefault(e => e.Id == id));
        }

        public Task<List<ProductModel>> GetAll()
        {
            return Task.FromResult(ProductModels);
        }

        public Task<Guid> Insert(ProductModel entity)
        {
            ProductModels.Add(entity);
            return Task.FromResult(Guid.NewGuid());
        }

        public Task Update(ProductModel entity)
        {
            return Task.CompletedTask;
        }
    }
}
