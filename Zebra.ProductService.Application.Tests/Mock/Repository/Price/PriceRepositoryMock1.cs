using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Persistance.Repository.Price;

namespace Zebra.ProductService.Application.Tests.Mock.Repository.Price
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
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    ProductId = Guid.Parse("00000000-0000-0000-0001-000000000000"),
                    Tax = 10,
                    Cost = 10,
                    From = DateTime.Now.AddDays(10)
                },
                new PriceModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    ProductId = Guid.Parse("00000000-0000-0000-0001-000000000000"),
                    Tax = 11,
                    Cost = 11,
                    From = DateTime.Now.AddDays(5)
                },
                new PriceModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    ProductId = Guid.Parse("00000000-0000-0000-0002-000000000000"),
                    Tax = 11,
                    Cost = 11,
                    From = DateTime.Now
                }
            };
        }

        public Task Delete(PriceModel entity)
        {
            PriceModels.Remove(entity);
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
