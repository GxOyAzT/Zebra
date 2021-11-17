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
                new ProductModel() { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Name" }
            };

            mockMediator.Setup(m => m.Send(It.IsAny<GetAllProductsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockProductList);

            _mockMediator = mockMediator.Object;
        }

        [Fact]
        public async Task Test_Name_Already_Exists()
        {
            await Assert.ThrowsAsync<IncorrectInputFormatException>(() => new ValidateProductInputCommandHandler(_mockMediator).Handle(new ValidateProductInputCommand(Guid.Empty, "Name", "", "12345678985"), new CancellationToken()));
        }

        [Fact]
        public async Task Test_SameName_Update()
        {
            await new ValidateProductInputCommandHandler(_mockMediator).Handle(new ValidateProductInputCommand(Guid.Parse("00000000-0000-0000-0000-000000000001"), "NAME", "", "12345678985"), new CancellationToken());
        }

        [Fact]
        public async Task Test_Name_Already_Exists_With_UpperCase()
        {
            await Assert.ThrowsAsync<IncorrectInputFormatException>(() => new ValidateProductInputCommandHandler(_mockMediator).Handle(new ValidateProductInputCommand(Guid.Empty, "NAME", "", "12345678985"), new CancellationToken()));
        }

        [Fact]
        public async Task Test_Description_Is_TooLong()
        {
            await Assert.ThrowsAsync<IncorrectInputFormatException>(() => new ValidateProductInputCommandHandler(_mockMediator).Handle(new ValidateProductInputCommand(Guid.Empty, "Name", "TestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTe101", "12345678985"), new CancellationToken()));
        }

        [Fact]
        public async Task Test_Ean_Incorrect()
        {
            await Assert.ThrowsAsync<IncorrectInputFormatException>(() => new ValidateProductInputCommandHandler(_mockMediator).Handle(new ValidateProductInputCommand(Guid.Empty, "Valid Name", "Valid description", "a12354874121"), new CancellationToken()));
        }

        [Fact]
        public async Task Test_Ok()
        {
            await new ValidateProductInputCommandHandler(_mockMediator).Handle(new ValidateProductInputCommand(Guid.Empty, "correct_input_name", "", "12345678985"), new CancellationToken());
        }
    }
}
