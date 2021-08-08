using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zebra.ProductService.Application.Features.Rating.Queries;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Persistance.Repository.Rating;

namespace Zebra.ProductService.Application.Tests.Features.Rating.Queries.UnitTests
{
    public class GetRatingsForProductQueryHandlerTests
    {
        IRatingRepository ratingRepository;

        public GetRatingsForProductQueryHandlerTests()
        {
            var mockRepo = new Mock<IRatingRepository>();

            mockRepo.Setup(m => m.GetAll())
                .ReturnsAsync(new List<RatingModel>()
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
                    });

            ratingRepository = mockRepo.Object;
        }

        [Fact]
        public async Task Test_Ok_One()
        {
            var request = new GetRatingsForProductQuery(Guid.Parse("00000000-0000-0000-0000-000000000001"));

            var result = await new GetRatingsForProductQueryHandler(ratingRepository).Handle(request, new CancellationToken());

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task Test_Ok_Two()
        {
            var request = new GetRatingsForProductQuery(Guid.Parse("00000000-0000-0000-0000-000000000002"));

            var result = await new GetRatingsForProductQueryHandler(ratingRepository).Handle(request, new CancellationToken());

            Assert.Single(result);
        }

        [Fact]
        public async Task Test_No_Rating_OfProductId()
        {
            var request = new GetRatingsForProductQuery(Guid.Parse("00000000-0000-0000-0000-000000000011"));

            var result = await new GetRatingsForProductQueryHandler(ratingRepository).Handle(request, new CancellationToken());

            Assert.Empty(result);
        }
    }
}
