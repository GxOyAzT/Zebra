using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Xunit;
using Zebra.ProductService.API.Tests.Mock.Repositories.Product;
using Zebra.ProductService.Application;
using Zebra.ProductService.Persistance.Repository.Product;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Zebra.ProductService.Application.Features.Product.Commands.RequestEntry;
using System;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using Zebra.Shared.LoggerDriver.DIConfiguration;

namespace Zebra.ProductService.API.Tests.Controllers.ProductManagementController.IntegrationTests
{
    public class UpdateProductTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public UpdateProductTests(CustomWebApplicationFactory<Startup> factory)
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
        public async Task Test_Ok()
        {
            var requestBody = new UpdateProductCommand(Guid.Parse("00000000-0000-0000-0000-000000000002"), "Product 2 after update", "", true);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_client.BaseAddress, "api/ProductManagement/getproduct"),
                Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json")
            };

            var response = await _client.PostAsync("api/ProductManagement/updateproduct", new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Test_Product_OfId_DoesNotExists()
        {
            var requestBody = new UpdateProductCommand(Guid.Parse("00000000-0000-0000-0000-000000000000"), "Incorrect guid", "", true);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_client.BaseAddress, "api/ProductManagement/getproduct"),
                Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json")
            };

            var response = await _client.PostAsync("api/ProductManagement/updateproduct", new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Test_Product_IncorrectInputFormat()
        {
            var requestBody = new UpdateProductCommand(Guid.Parse("00000000-0000-0000-0000-000000000001"), "", "", true);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_client.BaseAddress, "api/ProductManagement/getproduct"),
                Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json")
            };

            var response = await _client.PostAsync("api/ProductManagement/updateproduct", new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
