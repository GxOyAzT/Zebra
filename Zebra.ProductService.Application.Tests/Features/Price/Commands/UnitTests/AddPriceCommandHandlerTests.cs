using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zebra.ProductService.Application.Features.Price.Commands;
using Zebra.ProductService.Application.Features.Price.Queries;
using Zebra.ProductService.Application.Features.Product.Queries;
using Zebra.ProductService.Application.Tests.Mock.Repository.Price;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Domain.Exceptions;

namespace Zebra.ProductService.Application.Tests.Features.Price.Commands.UnitTests
{
    public class AddPriceCommandHandlerTests
    {
        List<PriceModel> Prices { get; }
        public AddPriceCommandHandlerTests()
        {
            Prices = new List<PriceModel>()
            {
                new PriceModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0001-000000000000"),
                    Cost = 1m,
                    Tax = 18,
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    From = DateTime.Today.AddDays(2)
                },
                new PriceModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0002-000000000000"),
                    Cost = 1m,
                    Tax = 18,
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    From = DateTime.Today.AddDays(-120)
                },
                new PriceModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0003-000000000000"),
                    Cost = 1m,
                    Tax = 18,
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    From = DateTime.Today.AddDays(10)
                }
            };
        }

        [Fact]
        public async Task Test_ProductNotExists()
        {
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .Throws<CannotFindEntityException>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetAllProductPricesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Prices);

            var request = new AddPriceCommand(Guid.Empty, 10, 10, DateTime.Now);

            await Assert.ThrowsAsync<CannotFindEntityException>(() => new AddPriceCommandHandler(mockMediator.Object, new PriceRepositoryMock1()).Handle(request, new CancellationToken()));
        }

        [Fact]
        public async Task Test_IncorrectTax_TooLow()
        {
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ProductModel());

            mockMediator.Setup(m => m.Send(It.IsAny<GetAllProductPricesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Prices);

            var request = new AddPriceCommand(Guid.Empty, 0, 10, DateTime.Now);

            await Assert.ThrowsAsync<IncorrectInputFormatException>(() => new AddPriceCommandHandler(mockMediator.Object, new PriceRepositoryMock1()).Handle(request, new CancellationToken()));
        }

        [Fact]
        public async Task Test_IncorrectTax_TooHigh()
        {
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ProductModel());

            mockMediator.Setup(m => m.Send(It.IsAny<GetAllProductPricesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Prices);

            var request = new AddPriceCommand(Guid.Empty, 100, 10, DateTime.Now);

            await Assert.ThrowsAsync<IncorrectInputFormatException>(() => new AddPriceCommandHandler(mockMediator.Object, new PriceRepositoryMock1()).Handle(request, new CancellationToken()));
        }

        [Fact]
        public async Task Test_IncorrectPrice_PriceTooLow()
        {
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ProductModel());

            mockMediator.Setup(m => m.Send(It.IsAny<GetAllProductPricesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Prices);

            var request = new AddPriceCommand(Guid.Empty, 50, 0, DateTime.Now);

            await Assert.ThrowsAsync<IncorrectInputFormatException>(() => new AddPriceCommandHandler(mockMediator.Object, new PriceRepositoryMock1()).Handle(request, new CancellationToken()));
        }

        [Fact]
        public async Task Test_IncorrectPremiereDate_TooEarly()
        {
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ProductModel());

            mockMediator.Setup(m => m.Send(It.IsAny<GetAllProductPricesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Prices);

            var request = new AddPriceCommand(Guid.Empty, 50, 20, DateTime.Now);

            await Assert.ThrowsAsync<IncorrectInputFormatException>(() => new AddPriceCommandHandler(mockMediator.Object, new PriceRepositoryMock1()).Handle(request, new CancellationToken()));
        }

        [Fact]
        public async Task Test_IncorrectPremiereDate_TooEarly_Two()
        {
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ProductModel());

            mockMediator.Setup(m => m.Send(It.IsAny<GetAllProductPricesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Prices);

            var request = new AddPriceCommand(Guid.Empty, 50, 20, DateTime.Now.AddDays(1).Date.AddHours(-2));

            await Assert.ThrowsAsync<IncorrectInputFormatException>(() => new AddPriceCommandHandler(mockMediator.Object, new PriceRepositoryMock1()).Handle(request, new CancellationToken()));
        }

        [Fact]
        public async Task Test_Ok()
        {
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ProductModel());

            mockMediator.Setup(m => m.Send(It.IsAny<GetAllProductPricesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Prices);

            var request = new AddPriceCommand(Guid.Empty, 50, 20, DateTime.Now.AddDays(1));

            var priceRepo = new PriceRepositoryMock1();

            await new AddPriceCommandHandler(mockMediator.Object, priceRepo).Handle(request, new CancellationToken());

            Assert.Equal(4, (await priceRepo.GetAll()).Count);
        }

        [Fact]
        public async Task Test_PremiereExists()
        {
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ProductModel());

            mockMediator.Setup(m => m.Send(It.IsAny<GetAllProductPricesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Prices);

            var request = new AddPriceCommand(Guid.Empty, 50, 20, DateTime.Now.AddDays(2));

            await Assert.ThrowsAsync<DomainRulesException>(() => new AddPriceCommandHandler(mockMediator.Object, new PriceRepositoryMock1()).Handle(request, new CancellationToken()));
        }
    }
}
