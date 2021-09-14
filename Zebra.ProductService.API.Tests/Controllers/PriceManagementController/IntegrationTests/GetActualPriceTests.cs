using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Zebra.ProductService.API.Tests.Mock.Repositories.Product;
using Zebra.ProductService.Application;
using Zebra.ProductService.Persistance.Repository.Product;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Zebra.ProductService.Application.Features.Price.Queries;
using Newtonsoft.Json;
using Zebra.ProductService.Domain.Entities;
using System.Net;
using Zebra.ProductService.Persistance.Repository.Price;
using Zebra.ProductService.API.Tests.Mock.Repositories.Price;
using Zebra.Shared.LoggerDriver.DIConfiguration;

namespace Zebra.ProductService.API.Tests.Controllers.PriceManagementController.IntegrationTests
{
    public class GetActualPriceTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public GetActualPriceTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddMediatR(typeof(MediaREntryPoint));

                    services.AddScoped<IProductRepository, ProductRepositoryMock1>();
                    services.AddScoped<IPriceRepository, PriceRepositoryMock1>();

                    services.ConfigureLoggerDriver("ProductService", false);
                });
            }).CreateClient(new WebApplicationFactoryClientOptions());
        }

        [Fact]
        public async Task Test_Ok()
        {
            var requestBody = new GetActualPriceQuery(Guid.Parse("00000000-0000-0000-0000-000000000002"));

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_client.BaseAddress, "api/pricemanagement/getactualproductprice"),
                Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(request);

            var responseContent = JsonConvert.DeserializeObject<PriceModel>(await response.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(Guid.Parse("00000000-0000-0000-0002-000000000000"), responseContent.Id);
        }

        [Fact]
        public async Task Test_Product_OfId_NotExists()
        {
            var requestBody = new GetActualPriceQuery(Guid.Parse("00000000-0000-0000-0000-000000000000"));

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_client.BaseAddress, "api/pricemanagement/getactualproductprice"),
                Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
