using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zebra.ProductService.Application.Features.Price.Commands;
using Zebra.ProductService.Application.Tests.Mock.Repository.Price;
using Zebra.ProductService.Domain.Exceptions;

namespace Zebra.ProductService.Application.Tests.Features.Price.Commands.UnitTests
{
    public class DeletePriceCommandHandlerTests
    {
        [Fact]
        public async Task Test_PriceNotExists()
        {
            var mockPriceRepo = new PriceRepositoryMock1();

            var request = new DeletePriceCommand(Guid.Parse("00000000-0000-0000-0000-000000000000"));

            await Assert.ThrowsAsync<CannotFindEntityException>(() => new DeletePriceCommandHandler(mockPriceRepo).Handle(request, new CancellationToken()));
        }

        [Fact]
        public async Task Test_PremiereDate_IsTooEarly()
        {
            var mockPriceRepo = new PriceRepositoryMock1();

            var request = new DeletePriceCommand(Guid.Parse("00000000-0000-0000-0000-000000000003"));

            await Assert.ThrowsAsync<DomainRulesException>(() => new DeletePriceCommandHandler(mockPriceRepo).Handle(request, new CancellationToken()));
        }

        [Fact]
        public async Task Test_PremiereDate_Ok()
        {
            var mockPriceRepo = new PriceRepositoryMock1();

            var request = new DeletePriceCommand(Guid.Parse("00000000-0000-0000-0000-000000000001"));

            await new DeletePriceCommandHandler(mockPriceRepo).Handle(request, new CancellationToken());

            Assert.Null(await mockPriceRepo.Get(Guid.Parse("00000000-0000-0000-0000-000000000001")));
        }
    }
}
