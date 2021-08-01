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
                    ProductModelId = Guid.Parse("00000000-0000-0000-0001-000000000000"),
                    Tax = 10,
                    Cost = 10,
                    From = DateTime.Now.AddDays(10)
                },
                new PriceModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    ProductModelId = Guid.Parse("00000000-0000-0000-0001-000000000000"),
                    Tax = 11,
                    Cost = 11,
                    From = DateTime.Now.AddDays(5)
                }
            };
        }

        public Task Delete(PriceModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<PriceModel> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PriceModel>> GetAll()
        {
            return Task.FromResult(PriceModels);
        }

        public Task Insert(PriceModel entity)
        {
            PriceModels.Add(entity);
            return Task.CompletedTask;
        }

        public Task Update(PriceModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
