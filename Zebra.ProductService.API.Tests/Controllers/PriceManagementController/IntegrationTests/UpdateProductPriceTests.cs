using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Xunit;
using Zebra.ProductService.API.Tests.Mock.Repositories.Price;
using Zebra.ProductService.API.Tests.Mock.Repositories.Product;
using Zebra.ProductService.Application;
using Zebra.ProductService.Persistance.Repository.Price;
using Zebra.ProductService.Persistance.Repository.Product;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System;
using Zebra.ProductService.Application.Features.Price.Commands;
using Newtonsoft.Json;
using System.Text;
using System.Net;

namespace Zebra.ProductService.API.Tests.Controllers.PriceManagementController.IntegrationTests
{
    public class UpdateProductPriceTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public UpdateProductPriceTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddMediatR(typeof(MediaREntryPoint));

                    services.AddScoped<IProductRepository, ProductRepositoryMock1>();
                    services.AddScoped<IPriceRepository, PriceRepositoryMock1>();
                });
            }).CreateClient(new WebApplicationFactoryClientOptions());
        }

        [Fact]
        public async Task Test_Ok()
        {
            var requestBody = new AddPriceCommand(Guid.Parse("00000000-0000-0000-0000-000000000002"), 10, 10, DateTime.Now.AddDays(1));

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_client.BaseAddress, "api/pricemanagement/updateproductprice"),
                Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Test_Product_OfId_NotExists()
        {
            var requestBody = new AddPriceCommand(Guid.Parse("00000000-0000-0000-0000-000000000011"), 10, 10, DateTime.Now.AddDays(1));

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_client.BaseAddress, "api/pricemanagement/updateproductprice"),
                Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Test_IncorrectDate_TooEarly()
        {
            var requestBody = new AddPriceCommand(Guid.Parse("00000000-0000-0000-0000-000000000011"), 10, 10, DateTime.Now);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_client.BaseAddress, "api/pricemanagement/updateproductprice"),
                Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Test_IncorrectTaxFormat()
        {
            var requestBody = new AddPriceCommand(Guid.Parse("00000000-0000-0000-0000-000000000011"), 100, 10, DateTime.Now);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_client.BaseAddress, "api/pricemanagement/updateproductprice"),
                Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
