using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Persistance.Repository.Price;

namespace Zebra.ProductService.API.Tests.Mock.Repositories.Price
{
    public class PriceRepositoryMock1 : IPriceRepository
    {
        List<PriceModel> PriceModels { get; set; }

        public PriceRepositoryMock1()
        {
            PriceModels = new List<PriceModel>()
            {
                new PriceModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0001-000000000000"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Cost = 10,
                    Tax = 10,
                    From = DateTime.Now.AddDays(-2)
                },
                new PriceModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0002-000000000000"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Cost = 20,
                    Tax = 20,
                    From = DateTime.Now.AddDays(-5)
                },
                new PriceModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0003-000000000000"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Cost = 30,
                    Tax = 30,
                    From = DateTime.Now.AddDays(-10)
                },
                new PriceModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0004-000000000000"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Cost = 40,
                    Tax = 40,
                    From = DateTime.Now.AddDays(6)
                }
            };
        }

        public Task Delete(PriceModel entity)
        {
            return Task.CompletedTask;
        }

        public Task<PriceModel> Get(Guid id)
        {
            return Task.FromResult(PriceModels.FirstOrDefault(e => e.Id == id));
        }

        public Task<List<PriceModel>> GetAll()
        {
            return Task.FromResult(PriceModels);
        }

        public Task<Guid> Insert(PriceModel entity)
        {
            PriceModels.Add(entity);
            return Task.FromResult(Guid.NewGuid());
        }

        public Task Update(PriceModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
