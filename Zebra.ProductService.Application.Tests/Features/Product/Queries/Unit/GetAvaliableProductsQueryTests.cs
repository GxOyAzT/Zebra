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
using Zebra.ProductService.Persistance.Repository.Product;

namespace Zebra.ProductService.Application.Tests.Features.Product.Queries.Unit
{
    public class GetAvaliableProductsQueryTests
    {
        Mock<IMediator> mockMediator = new Mock<IMediator>();

        IMediator _mockProductRepository;

        List<ProductModel> mockProductList = new List<ProductModel>()
            {
                new ProductModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Name = "Name 1",
                    IsInSale = true
                },
                new ProductModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Name = "Name 2",
                    IsInSale = true
                },
                new ProductModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    Name = "Name 3",
                    IsInSale = false
                },
                new ProductModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    Name = "Name 4",
                    IsInSale = true
                },
                new ProductModel()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    Name = "Name 5",
                    IsInSale = false
                },
            };

        public GetAvaliableProductsQueryTests()
        {
            
        }

        [Fact]
        public async Task Ok()
        {
            mockMediator.Setup(m => m.Send(It.IsAny<GetAllProductsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockProductList);

            mockMediator.Setup(m => m.Send(It.IsAny<GetActualPriceQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PriceModel() { Id = Guid.Empty, Cost = 20 });

            _mockProductRepository = mockMediator.Object;

            var output = await new GetAvaliableProductsQueryHandler(_mockProductRepository)
                .Handle(new GetAvaliableProductsQuery(), new CancellationToken());

            Assert.Equal(3, output.Count);
            Assert.Equal(20, output[0].Cost);
            Assert.Equal(20, output[1].Cost);
            Assert.Equal(20, output[2].Cost);
        }

        [Fact]
        public async Task Ok_2()
        {
            mockMediator.Setup(m => m.Send(It.IsAny<GetAllProductsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockProductList);

            mockMediator.Setup(m => m.Send(It.IsAny<GetActualPriceQuery>(), It.IsAny<CancellationToken>()))
                .Throws<CollectionIsEmptyException>();

            _mockProductRepository = mockMediator.Object;

            var output = await new GetAvaliableProductsQueryHandler(_mockProductRepository)
                .Handle(new GetAvaliableProductsQuery(), new CancellationToken());

            Assert.Empty(output);
        }

        [Fact]
        public async Task Ok_3()
        {
            mockMediator.Setup(m => m.Send(It.IsAny<GetAllProductsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockProductList);

            mockMediator.Setup(m => m.Send(It.IsAny<GetActualPriceQuery>(), It.IsAny<CancellationToken>()))
                .Throws<DivideByZeroException>();

            _mockProductRepository = mockMediator.Object;

            await Assert.ThrowsAsync<DivideByZeroException>(() => new GetAvaliableProductsQueryHandler(_mockProductRepository)
                .Handle(new GetAvaliableProductsQuery(), new CancellationToken()));
        }
    }
}
