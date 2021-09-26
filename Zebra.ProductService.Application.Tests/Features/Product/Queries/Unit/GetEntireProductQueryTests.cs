using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zebra.ProductService.Application.Features.Price.Queries;
using Zebra.ProductService.Application.Features.Product.Queries;
using Zebra.ProductService.Application.Features.Rating.Queries;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Domain.Exceptions;

namespace Zebra.ProductService.Application.Tests.Features.Product.Queries.Unit
{
    public class GetEntireProductQueryTests
    {
        public GetEntireProductQueryTests()
        {
        }

        [Fact]
        public async Task TestA_Ok()
        {
            var prices = new List<PriceModel>()
            {
                new PriceModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0001-000000000000"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Cost = 1,
                    From = DateTime.Today.AddDays(-10)
                },
                new PriceModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0002-000000000000"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Cost = 2,
                    From = DateTime.Today.AddDays(-20)
                },
                new PriceModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0003-000000000000"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Cost = 10,
                    From = DateTime.Today.AddDays(10)
                },
            };

            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ProductModel() { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Product -1" });

            mockMediator.Setup(m => m.Send(It.IsAny<GetAllProductPricesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(prices);

            mockMediator.Setup(m => m.Send(It.IsAny<GetRatingsForProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<RatingModel>());

            var entireProduct = await new GetEntireProductQueryHandler(mockMediator.Object).Handle(new GetEntireProductQuery(Guid.Parse("00000000-0000-0000-0000-000000000001")), new CancellationToken());

            Assert.Equal(Guid.Parse("00000000-0000-0000-0000-000000000001"), entireProduct.Product.Id);

            Assert.Single(entireProduct.HistoryPrices);
            Assert.Single(entireProduct.PricePremieres);
            Assert.Equal(Guid.Parse("00000000-0000-0000-0001-000000000000"), entireProduct.ActualPrice.Id);

            Assert.Empty(entireProduct.Ratings);
            Assert.Equal(0, entireProduct.AverageRating);
        }

        [Fact]
        public async Task TestB_Ok()
        {
            var prices = new List<PriceModel>()
            {
                new PriceModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0001-000000000000"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Cost = 1,
                    From = DateTime.Today.AddDays(-10)
                },
                new PriceModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0002-000000000000"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Cost = 2,
                    From = DateTime.Today.AddDays(20)
                },
                new PriceModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0003-000000000000"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Cost = 10,
                    From = DateTime.Today.AddDays(10)
                },
            };

            var ratings = new List<RatingModel>()
            {
                new RatingModel()
                {
                    Id = Guid.Parse("00000000-0000-0001-0000-000000000000"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Score = 4
                },
                new RatingModel()
                {
                    Id = Guid.Parse("00000000-0000-0002-0000-000000000000"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Score = 5
                }
            };

            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ProductModel() { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Product -1" });

            mockMediator.Setup(m => m.Send(It.IsAny<GetAllProductPricesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(prices);

            mockMediator.Setup(m => m.Send(It.IsAny<GetRatingsForProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ratings);

            var entireProduct = await new GetEntireProductQueryHandler(mockMediator.Object).Handle(new GetEntireProductQuery(Guid.Parse("00000000-0000-0000-0000-000000000001")), new CancellationToken());

            Assert.Equal(Guid.Parse("00000000-0000-0000-0000-000000000001"), entireProduct.Product.Id);

            Assert.Empty(entireProduct.HistoryPrices);
            Assert.Equal(2, entireProduct.PricePremieres.Count);
            Assert.Equal(Guid.Parse("00000000-0000-0000-0001-000000000000"), entireProduct.ActualPrice.Id);

            Assert.Equal(2, entireProduct.Ratings.Count);
            Assert.Equal(4.5f, entireProduct.AverageRating);
        }

        [Fact]
        public async Task TestC_Ok_NoActualPrice()
        {
            var prices = new List<PriceModel>()
            {
                new PriceModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0001-000000000000"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Cost = 1,
                    From = DateTime.Today.AddDays(-12)
                }
            };

            var ratings = new List<RatingModel>()
            {
                new RatingModel()
                {
                    Id = Guid.Parse("00000000-0000-0001-0000-000000000000"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Score = 4
                },
                new RatingModel()
                {
                    Id = Guid.Parse("00000000-0000-0002-0000-000000000000"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Score = 5
                }
            };

            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ProductModel() { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Product -1" });

            mockMediator.Setup(m => m.Send(It.IsAny<GetAllProductPricesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(prices);

            mockMediator.Setup(m => m.Send(It.IsAny<GetRatingsForProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ratings);

            var entireProduct = await new GetEntireProductQueryHandler(mockMediator.Object).Handle(new GetEntireProductQuery(Guid.Parse("00000000-0000-0000-0000-000000000001")), new CancellationToken());

            Assert.Equal(Guid.Parse("00000000-0000-0000-0000-000000000001"), entireProduct.Product.Id);

            Assert.Empty(entireProduct.HistoryPrices);
            Assert.Empty(entireProduct.PricePremieres);
            Assert.Equal(Guid.Parse("00000000-0000-0000-0001-000000000000"), entireProduct.ActualPrice.Id);

            Assert.Equal(2, entireProduct.Ratings.Count);
            Assert.Equal(4.5f, entireProduct.AverageRating);
        }

        [Fact]
        public async Task TestD_Ok_NoPrices()
        {
            var ratings = new List<RatingModel>()
            {
                new RatingModel()
                {
                    Id = Guid.Parse("00000000-0000-0001-0000-000000000000"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Score = 4
                },
                new RatingModel()
                {
                    Id = Guid.Parse("00000000-0000-0002-0000-000000000000"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Score = 5
                }
            };

            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ProductModel() { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Product -1" });

            mockMediator.Setup(m => m.Send(It.IsAny<GetAllProductPricesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<PriceModel>());

            mockMediator.Setup(m => m.Send(It.IsAny<GetRatingsForProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ratings);

            var entireProduct = await new GetEntireProductQueryHandler(mockMediator.Object).Handle(new GetEntireProductQuery(Guid.Parse("00000000-0000-0000-0000-000000000001")), new CancellationToken());

            Assert.Equal(Guid.Parse("00000000-0000-0000-0000-000000000001"), entireProduct.Product.Id);

            Assert.Empty(entireProduct.HistoryPrices);
            Assert.Empty(entireProduct.PricePremieres);
            Assert.Null(entireProduct.ActualPrice);

            Assert.Equal(2, entireProduct.Ratings.Count);
            Assert.Equal(4.5f, entireProduct.AverageRating);
        }

        [Fact]
        public async Task TestE_Ok_NoProduct()
        {
            var ratings = new List<RatingModel>()
            {
                new RatingModel()
                {
                    Id = Guid.Parse("00000000-0000-0001-0000-000000000000"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Score = 4
                },
                new RatingModel()
                {
                    Id = Guid.Parse("00000000-0000-0002-0000-000000000000"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Score = 5
                }
            };

            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new CannotFindEntityException());

            mockMediator.Setup(m => m.Send(It.IsAny<GetAllProductPricesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<PriceModel>());

            mockMediator.Setup(m => m.Send(It.IsAny<GetRatingsForProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ratings);

            Assert.ThrowsAsync<CannotFindEntityException>(() => new GetEntireProductQueryHandler(mockMediator.Object).Handle(new GetEntireProductQuery(Guid.Parse("00000000-0000-0000-0000-000000000001")), new CancellationToken()));
        }
    }
}
