using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zebra.ProductService.Application.Features.Price.Queries;
using Zebra.ProductService.Application.Features.Product.Queries;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Domain.Exceptions;

namespace Zebra.ProductService.Application.Tests.Features.Price.Queries.UnitTests
{
    public class GetActualPriceQueryHandlerTests
    {
        [Fact]
        public async Task Test_NoRelatedproduct()
        {
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .Throws<CannotFindEntityException>();

            var request = new GetActualPriceQuery(Guid.Empty);

            await Assert.ThrowsAsync<CannotFindEntityException>(() => new GetActualPriceQueryHandler(mockMediator.Object).Handle(request, new CancellationToken()));
        }

        [Fact]
        public async Task Test_NoPrice_RelatedToProduct()
        {
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ProductModel());

            List<PriceModel> priceModels = new List<PriceModel>()
            {
            };

            mockMediator.Setup(m => m.Send(It.IsAny<GetAllProductPricesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(priceModels);

            var request = new GetActualPriceQuery(Guid.Empty);

            await Assert.ThrowsAsync<CollectionIsEmptyException>(() => new GetActualPriceQueryHandler(mockMediator.Object).Handle(request, new CancellationToken()));
        }

        [Fact]
        public async Task Test_NoPrice_ThrereIsOnlyPlanned_Price()
        {
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ProductModel());

            List<PriceModel> priceModels = new List<PriceModel>()
            {
                new PriceModel()
                {
                    From = DateTime.Now.AddDays(2)
                }
            };

            mockMediator.Setup(m => m.Send(It.IsAny<GetAllProductPricesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(priceModels);

            var request = new GetActualPriceQuery(Guid.Empty);

            await Assert.ThrowsAsync<CannotFindEntityException>(() => new GetActualPriceQueryHandler(mockMediator.Object).Handle(request, new CancellationToken()));
        }

        [Fact]
        public async Task Test_Ok_OnePriceInHistory()
        {
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ProductModel());

            List<PriceModel> priceModels = new List<PriceModel>()
            {
                new PriceModel()
                {
                    From = DateTime.Now.AddDays(2)
                },
                new PriceModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    From = DateTime.Now.AddDays(-2)
                }
            };

            mockMediator.Setup(m => m.Send(It.IsAny<GetAllProductPricesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(priceModels);

            var request = new GetActualPriceQuery(Guid.Empty);

            var actualPrice = await new GetActualPriceQueryHandler(mockMediator.Object).Handle(request, new CancellationToken());

            Assert.Equal(Guid.Parse("00000000-0000-0000-0000-000000000001"), actualPrice.Id);
        }

        [Fact]
        public async Task Test_Ok_SeveralPricesInHistory()
        {
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ProductModel());

            List<PriceModel> priceModels = new List<PriceModel>()
            {
                new PriceModel()
                {
                    From = DateTime.Now.AddDays(2)
                },
                new PriceModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    From = DateTime.Now.AddDays(-2)
                },
                new PriceModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    From = DateTime.Now.AddDays(-5)
                },
                new PriceModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    From = DateTime.Now.AddDays(-1)
                }
            };

            mockMediator.Setup(m => m.Send(It.IsAny<GetAllProductPricesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(priceModels);

            var request = new GetActualPriceQuery(Guid.Empty);

            var actualPrice = await new GetActualPriceQueryHandler(mockMediator.Object).Handle(request, new CancellationToken());

            Assert.Equal(Guid.Parse("00000000-0000-0000-0000-000000000003"), actualPrice.Id);
        }
    }
}
