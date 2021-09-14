using System.Net.Http;
using Xunit;
using Zebra.ProductService.API.Tests.Mock.Repositories.Product;
using Zebra.ProductService.Application;
using Zebra.ProductService.Persistance.Repository.Product;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Zebra.ProductService.Persistance.Repository.Price;
using Zebra.ProductService.API.Tests.Mock.Repositories.Price;
using Zebra.Shared.LoggerDriver.DIConfiguration;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System;
using Zebra.ProductService.Domain.Entities;
using System.Collections.Generic;
using System.Net;

namespace Zebra.ProductService.API.Tests.Controllers.ProductClientController.IntegrationTests
{
    public class GetAllAvaliableProductsTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public GetAllAvaliableProductsTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddMediatR(typeof(MediaREntryPoint));

                    services.AddScoped<IProductRepository, ProductRepositoryMock1>();
                    services.AddScoped<IPriceRepository, PriceRepositoryMock1>();

                    services.ConfigureLoggerDriver("ProductService", false);
                });
            }).CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions());
        }

        [Fact]
        public async Task Ok()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_client.BaseAddress, "api/productclient/getallavaliableproducts")
            };

            var response = await _client.SendAsync(request);

            var responseContent = JsonConvert.DeserializeObject<List<ProductModel>>(await response.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(2, responseContent.Count);
        }
    }
}
