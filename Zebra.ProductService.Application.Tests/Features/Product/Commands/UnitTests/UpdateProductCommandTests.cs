using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zebra.ProductService.Application.Features.Product.Commands.RequestEntry;
using Zebra.ProductService.Application.Features.Product.Commands.Validation;
using Zebra.ProductService.Application.Features.Product.Queries;
using Zebra.ProductService.Application.Tests.Mock.Repository.Product;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Domain.Exceptions;

namespace Zebra.ProductService.Application.Tests.Features.Product.Commands.UnitTests
{
    public class UpdateProductCommandTests
    {
        [Fact]
        public async Task Test_ValidationThrowsException()
        {
            // Mock mediator configuration
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<ValidateProductInputCommand>(), It.IsAny<CancellationToken>()))
                .Throws<IncorrectInputFormatException>();
            var mediator = mockMediator.Object;

            var mockProductRepo = new ProductRepositoryMock1();

            // create request
            var request = new UpdateProductCommand(Guid.Empty, "Name", "", true);

            // Actual test
            await Assert.ThrowsAsync<IncorrectInputFormatException>(() => new UpdateProductCommandHandler(mediator, mockProductRepo).Handle(request, new CancellationToken()));
        }

        [Fact]
        public async Task Test_ProductDoNotExists()
        {
            // Mock mediator configuration
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<ValidateProductInputCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);
            mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .Throws<CannotFindEntityException>();
            var mediator = mockMediator.Object;

            var mockProductRepo = new ProductRepositoryMock1();

            // create request
            var request = new UpdateProductCommand(Guid.Empty, "valid_product_name", "", true);

            // Actual test
            await Assert.ThrowsAsync<CannotFindEntityException>(() => new UpdateProductCommandHandler(mediator, mockProductRepo).Handle(request, new CancellationToken()));
        }

        [Fact]
        public async Task Test_Ok()
        {
            // Mock mediator configuration
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<ValidateProductInputCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);
            mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ProductModel());
            var mediator = mockMediator.Object;

            var mockProductRepo = new ProductRepositoryMock1();

            // create request
            var request = new UpdateProductCommand(Guid.Empty, "valid_product_name", "", true);

            // Actual test
            await new UpdateProductCommandHandler(mediator, mockProductRepo).Handle(request, new CancellationToken());
        }
    }
}
