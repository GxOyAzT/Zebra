using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Zebra.ProductService.API.Tests.Mock.Repositories.Product;
using Zebra.ProductService.Application;
using Zebra.ProductService.Application.Features.Product.Commands.RequestEntry;
using Zebra.ProductService.Persistance.Repository.Product;
using Zebra.Shared.LoggerDriver.DIConfiguration;

namespace Zebra.ProductService.API.Tests.Controllers.ProductManagementController.IntegrationTests
{
    public class AddProductTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public AddProductTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddMediatR(typeof(MediaREntryPoint));

                    services.AddScoped<IProductRepository, ProductRepositoryMock1>();

                    services.ConfigureLoggerDriver("ProductService", false);
                });
            }).CreateClient(new WebApplicationFactoryClientOptions());
        }

        [Fact]
        public async Task Ok()
        {
            var command = new AddProductCommand("New Product", "New product description.");

            var response = await _client.PostAsync("api/ProductManagement/addproduct", new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Empty_Name()
        {
            var command = new AddProductCommand("", "New product description.");

            var response = await _client.PostAsync("api/ProductManagement/addproduct", new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
