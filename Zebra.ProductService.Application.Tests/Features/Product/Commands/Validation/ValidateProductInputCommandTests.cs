using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zebra.ProductService.Application.Features.Product.Commands.Validation;
using Zebra.ProductService.Application.Features.Product.Queries;
using Zebra.ProductService.Application.Tests.Mock.Repository;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Domain.Exceptions;

namespace Zebra.ProductService.Application.Tests.Features.Product.Commands.Validation
{
    public class ValidateProductInputCommandTests
    {
        IMediator _mockMediator;

        public ValidateProductInputCommandTests()
        {
            var mockMediator = new Mock<IMediator>();

            List<ProductModel> mockProductList = new List<ProductModel>()
            {
                new ProductModel() { Name = "Name" }
            };

            mockMediator.Setup(m => m.Send(It.IsAny<GetAllProductsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockProductList);

            _mockMediator = mockMediator.Object;
        }

        [Fact]
        public async Task Test_Name_Already_Exists()
        {
            await Assert.ThrowsAsync<IncorrectInputFormatException>(() => new ValidateProductInputCommandHandler(_mockMediator).Handle(new ValidateProductInputCommand("Name", ""), new CancellationToken()));
        }

        [Fact]
        public async Task Test_Name_Already_Exists_With_UpperCase()
        {
            await Assert.ThrowsAsync<IncorrectInputFormatException>(() => new ValidateProductInputCommandHandler(_mockMediator).Handle(new ValidateProductInputCommand("NAME", ""), new CancellationToken()));
        }

        [Fact]
        public async Task Test_Description_Is_TooLong()
        {
            await Assert.ThrowsAsync<IncorrectInputFormatException>(() => new ValidateProductInputCommandHandler(_mockMediator).Handle(new ValidateProductInputCommand("Name", "TestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTe101"), new CancellationToken()));
        }

        [Fact]
        public async Task Test_Ok()
        {
            await new ValidateProductInputCommandHandler(_mockMediator).Handle(new ValidateProductInputCommand("correct_input_name", ""), new CancellationToken());
        }
    }
}
