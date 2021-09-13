using Moq;
using System;
using Xunit;
using Zebra.CustomerService.Application.Customer;
using Zebra.CustomerService.Application.Validation.Customer;
using Zebra.CustomerService.Domain.Enums;
using Zebra.CustomerService.Domain.Models.ApiModels;
using Zebra.CustomerService.Domain.Models.Tables;
using Zebra.CustomerService.Persistance.Repository.Customer;

namespace Zebra.CustomerService.Application.Tests.Customer.InsertNewCustomerTests
{
    public class InsertNewCustomerTest
    {
        [Fact]
        public void Test_Ok()
        {
            var mockCustomerRepository = new Mock<ICustomerRepository>();
            mockCustomerRepository.Setup(m => m.Insert(It.IsAny<CustomerModel>()));

            var _testedUtility = new InsertNewCustomer(mockCustomerRepository.Object, new CustomerValidator());

            var customer = new CustomerApiModel()
            {
                FullName = "Full name",
                Street = "Street",
                City = "City",
                Dob = DateTime.Now.AddDays(-1000),
                Gender = GenderEnum.Female
            };

            _testedUtility.Execute(customer);

            Assert.Empty(_testedUtility.GetErrors());
        }

        [Fact]
        public void Test_EmptyFullName()
        {
            var mockCustomerRepository = new Mock<ICustomerRepository>();
            mockCustomerRepository.Setup(m => m.Insert(It.IsAny<CustomerModel>()));

            var _testedUtility = new InsertNewCustomer(mockCustomerRepository.Object, new CustomerValidator());

            var customer = new CustomerApiModel()
            {
                FullName = "",
                Street = "Street",
                City = "City",
                Dob = DateTime.Now.AddDays(-1000),
                Gender = GenderEnum.Female
            };

            _testedUtility.Execute(customer);

            Assert.NotEmpty(_testedUtility.GetErrors());
        }
    }
}
