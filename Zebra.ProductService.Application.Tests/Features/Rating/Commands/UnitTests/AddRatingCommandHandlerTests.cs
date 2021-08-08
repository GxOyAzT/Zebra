using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zebra.ProductService.Application.Features.Product.Queries;
using Zebra.ProductService.Application.Features.Rating.Commands;
using Zebra.ProductService.Application.Tests.Mock.Repository.Rating;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Domain.Exceptions;

namespace Zebra.ProductService.Application.Tests.Features.Rating.Commands.UnitTests
{
    public class AddRatingCommandHandlerTests
    {
        [Fact]
        public async Task Test_Product_OfId_NotExists()
        {
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .Throws<CannotFindEntityException>();

            var mediator = mockMediator.Object;

            var request = new AddRatingCommand(2, "Rewiew", System.Guid.Empty);

            await Assert.ThrowsAsync<CannotFindEntityException>(() => new AddRatingCommandHandler(new RatingRepositoryMock1(), mediator).Handle(request, new CancellationToken()));
        }

        [Fact]
        public async Task Test_Incorrect_Score_TooLow()
        {
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ProductModel());

            var mediator = mockMediator.Object;

            var request = new AddRatingCommand(0, "Rewiew", System.Guid.Empty);

            await Assert.ThrowsAsync<IncorrectInputFormatException>(() => new AddRatingCommandHandler(new RatingRepositoryMock1(), mediator).Handle(request, new CancellationToken()));
        }

        [Fact]
        public async Task Test_Incorrect_Score_TooHigh()
        {
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ProductModel());

            var mediator = mockMediator.Object;

            var request = new AddRatingCommand(6, "Rewiew", System.Guid.Empty);

            await Assert.ThrowsAsync<IncorrectInputFormatException>(() => new AddRatingCommandHandler(new RatingRepositoryMock1(), mediator).Handle(request, new CancellationToken()));
        }

        [Fact]
        public async Task Test_Incorrect_Review_TooLong()
        {
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ProductModel());

            var mediator = mockMediator.Object;

            var request = new AddRatingCommand(4, "Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test T", System.Guid.Empty);

            await Assert.ThrowsAsync<IncorrectInputFormatException>(() => new AddRatingCommandHandler(new RatingRepositoryMock1(), mediator).Handle(request, new CancellationToken()));
        }

        [Fact]
        public async Task Test_OK()
        {
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ProductModel());

            var mediator = mockMediator.Object;

            var mockRatingRepo = new RatingRepositoryMock1();

            var request = new AddRatingCommand(4, "Test Test Test", System.Guid.Empty);

            await new AddRatingCommandHandler(mockRatingRepo, mediator).Handle(request, new CancellationToken());

            Assert.Equal(3, (await mockRatingRepo.GetAll()).Count);
        }
    }
}
