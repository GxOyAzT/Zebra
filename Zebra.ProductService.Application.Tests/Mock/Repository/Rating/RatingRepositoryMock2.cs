using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Persistance.Repository.Rating;

namespace Zebra.ProductService.Application.Tests.Mock.Repository.Rating
{
    public class RatingRepositoryMock2 : IRatingRepository
    {
        public List<RatingModel> Ratings { get; set; }
        public RatingRepositoryMock2()
        {
            Ratings = new List<RatingModel>()
            {
                new RatingModel()
                {
                    Id = Guid.Parse("00000000-0001-0000-0000-000000000000"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Score = 4,
                    Review = "Ok product.",
                    AddDate = DateTime.Now.AddDays(-10)
                },
                new RatingModel()
                {
                    Id = Guid.Parse("00000000-0002-0000-0000-000000000000"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Score = 5,
                    Review = "Ok product.",
                    AddDate = DateTime.Now.AddDays(-11)
                },
                new RatingModel()
                {
                    Id = Guid.Parse("00000000-0003-0000-0000-000000000000"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Score = 5,
                    Review = "Ok product.",
                    AddDate = DateTime.Now.AddDays(-5)
                }
            };
        }


        public Task Delete(RatingModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<RatingModel> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<RatingModel>> GetAll()
        {
            return Task.FromResult(Ratings);
        }

        public Task<Guid> Insert(RatingModel entity)
        {
            Ratings.Add(entity);
            return Task.FromResult(Guid.NewGuid());
        }

        public Task Update(RatingModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
