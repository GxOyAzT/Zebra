using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zebra.ProductService.Application.Features.Product.Queries;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Domain.Exceptions;

namespace Zebra.ProductService.Application.Tests.Features.Product.Queries.Unit
{
    public class GetProductQueryHandlerTests
    {
        IMediator _mockMediator;

        public GetProductQueryHandlerTests()
        {
            var mockMediator = new Mock<IMediator>();

            List<ProductModel> mockProductList = new List<ProductModel>()
            {
                new ProductModel()
                {
                    Id = Guid.Parse("a905bd23-0641-428c-a3db-494fc038cddf"),
                    Name = "Product 1"
                },
                new ProductModel()
                {
                    Id = Guid.Parse("45d50983-24d8-4204-970d-787a14af38e2"),
                    Name = "Product 2"
                },
                new ProductModel()
                {
                    Id = Guid.Parse("5777a475-b837-41df-81f7-202093079dee"),
                    Name = "Product 3"
                },
            };

            mockMediator.Setup(m => m.Send(It.IsAny<GetAllProductsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockProductList);

            _mockMediator = mockMediator.Object;
        }

        [Fact]
        public async Task Test_Product_Of_Id_NotExists()
        {
            await Assert.ThrowsAsync<CannotFindEntityException>(() => new GetProductQueryHandler(_mockMediator)
                .Handle(new GetProductQuery(Guid.Empty), new CancellationToken()));
        }

        [Fact]
        public async Task Test_Ok()
        {
            var returnObj = await new GetProductQueryHandler(_mockMediator)
                .Handle(new GetProductQuery(Guid.Parse("45d50983-24d8-4204-970d-787a14af38e2")), new CancellationToken());

            Assert.Equal("Product 2", returnObj.Name);
        }
    }
}
