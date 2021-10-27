using Castle.Core.Configuration;
using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zebra.ProductService.Application.Features.Files;
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
            mockMediator.Setup(m => m.Send(It.IsAny<SaveFileCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync("");
            var mediator = mockMediator.Object;

            var relativeFilePathResolver = new Mock<IRelativeFilePathResolver>();
            relativeFilePathResolver.Setup(m => m.ProductImages).Returns("");

            var mockProductRepo = new ProductRepositoryMock1();

            // create request
            var request = new AddProductCommand("Name", "", "", "12345678930");

            // Actual test
            await Assert.ThrowsAsync<IncorrectInputFormatException>(() => new AddProductCommandHandler(mediator, mockProductRepo, relativeFilePathResolver.Object).Handle(request, new CancellationToken()));

            Assert.Single(await mockProductRepo.GetAll());
        }

        [Fact]
        public async Task Test_Ok()
        {
            // Mock mediator configuration
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<ValidateProductInputCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);
            mockMediator.Setup(m => m.Send(It.IsAny<SaveFileCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync("");
            var mediator = mockMediator.Object;

            var relativeFilePathResolver = new Mock<IRelativeFilePathResolver>();
            relativeFilePathResolver.Setup(m => m.ProductImages).Returns("");

            var mockProductRepo = new ProductRepositoryMock1();

            // create request
            var request = new AddProductCommand("Correct Name", "", "", "12345678930");

            // Actual test
            await new AddProductCommandHandler(mediator, mockProductRepo, relativeFilePathResolver.Object).Handle(request, new CancellationToken());

            // Check result
            Assert.Equal(2, (await mockProductRepo.GetAll()).Count);
        }
    }
}
