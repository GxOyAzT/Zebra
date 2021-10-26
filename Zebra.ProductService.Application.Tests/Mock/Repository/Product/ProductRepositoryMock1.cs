using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Persistance.Repository.Product;

namespace Zebra.ProductService.Application.Tests.Mock.Repository.Product
{
    public class ProductRepositoryMock1 : IProductRepository
    {
        List<ProductModel> ProductModels { get; set; }

        public ProductRepositoryMock1()
        {
            ProductModels = new List<ProductModel>()
            {
                new ProductModel()
                {
                    Id = Guid.Parse("d5eddbca-7029-4e9c-a2f1-107ff9b68782"),
                    Name = "Name"
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
            ProductModels.Add(entity);
            return Task.CompletedTask;
        }
    }
}
