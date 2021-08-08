using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Persistance.Repository.Rating;

namespace Zebra.ProductService.Application.Tests.Mock.Repository.Rating
{
    public class RatingRepositoryMock1 : IRatingRepository
    {
        public List<RatingModel> Ratings { get; set; }
        public RatingRepositoryMock1()
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

        public Task Insert(RatingModel entity)
        {
            Ratings.Add(entity);
            return Task.CompletedTask;
        }

        public Task Update(RatingModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
