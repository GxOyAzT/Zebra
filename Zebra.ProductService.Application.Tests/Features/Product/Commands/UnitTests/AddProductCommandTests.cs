using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zebra.ProductService.Application.Features.Product.Commands.RequestEntry;
using Zebra.ProductService.Application.Features.Product.Commands.Validation;
using Zebra.ProductService.Application.Tests.Mock.Repository.Product;
using Zebra.ProductService.Domain.Exceptions;

namespace Zebra.ProductService.Application.Tests.Features.Product.Commands.UnitTests
{
    public class AddProductCommandTests
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
            var request = new AddProductCommand("Name", "");

            // Actual test
            await Assert.ThrowsAsync<IncorrectInputFormatException>(() => new AddProductCommandHandler(mediator, mockProductRepo).Handle(request, new CancellationToken()));

            Assert.Single(await mockProductRepo.GetAll());
        }

        [Fact]
        public async Task Test_Ok()
        {
            // Mock mediator configuration
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<ValidateProductInputCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);
            var mediator = mockMediator.Object;

            var mockProductRepo = new ProductRepositoryMock1();

            // create request
            var request = new AddProductCommand("Correct Name", "");

            // Actual test
            await new AddProductCommandHandler(mediator, mockProductRepo).Handle(request, new CancellationToken());

            // Check result
            Assert.Equal(2, (await mockProductRepo.GetAll()).Count);
        }
    }
}
