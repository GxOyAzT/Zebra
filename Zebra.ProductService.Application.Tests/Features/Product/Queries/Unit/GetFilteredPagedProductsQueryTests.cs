using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zebra.ProductService.Application.Features.Product.Queries;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Domain.Enums;

namespace Zebra.ProductService.Application.Tests.Features.Product.Queries.Unit
{
    public class GetFilteredPagedProductsQueryTests
    {
        private List<ProductModel> ProductModels { get; }

        public GetFilteredPagedProductsQueryTests()
        {
            ProductModels = new List<ProductModel>();

            ProductModels.Add(new ProductModel()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Name = "Product 1",
                IsInSale = true,
                AddDate = DateTime.Today.AddDays(-10)
            });

            ProductModels.Add(new ProductModel()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                Name = "Product 2",
                IsInSale = true,
                AddDate = DateTime.Today.AddDays(-10)
            });

            ProductModels.Add(new ProductModel()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                Name = "Product 3",
                IsInSale = true,
                AddDate = DateTime.Today.AddDays(-10)
            });

            ProductModels.Add(new ProductModel()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                Name = "Product 4",
                IsInSale = false,
                AddDate = DateTime.Today.AddDays(-10)
            });

            ProductModels.Add(new ProductModel()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                Name = "Product 5",
                IsInSale = true,
                AddDate = DateTime.Today.AddDays(-10)
            });

            ProductModels.Add(new ProductModel()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                Name = "Product 6",
                IsInSale = false,
                AddDate = DateTime.Today.AddDays(-10)
            });

            ProductModels.Add(new ProductModel()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                Name = "Product 7",
                IsInSale = true,
                AddDate = DateTime.Today.AddDays(-10)
            });
        }

        [Fact]
        public async Task TestA()
        {
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetAllProductsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ProductModels);

            var result = await new GetFilteredPagedProductsQueryHandler(mockMediator.Object)
                .Handle(new GetFilteredPagedProductsQuery(String.Empty, IsInSaleFilterEnum.Ignore, 20, 1),
                new CancellationToken());

            Assert.Equal(7, result.Models.Count);
        }

        [Fact]
        public async Task TestB()
        {
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetAllProductsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ProductModels);

            var result = await new GetFilteredPagedProductsQueryHandler(mockMediator.Object)
                .Handle(new GetFilteredPagedProductsQuery(String.Empty, IsInSaleFilterEnum.InSale, 20, 1),
                new CancellationToken());

            Assert.Equal(5, result.Models.Count);
        }

        [Fact]
        public async Task TestC()
        {
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetAllProductsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ProductModels);

            var result = await new GetFilteredPagedProductsQueryHandler(mockMediator.Object)
                .Handle(new GetFilteredPagedProductsQuery(String.Empty, IsInSaleFilterEnum.InSale, 20, 1),
                new CancellationToken());

            Assert.Equal(5, result.Models.Count);
        }
    }
}
