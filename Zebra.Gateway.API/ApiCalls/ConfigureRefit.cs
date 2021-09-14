using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using Zebra.Gateway.API.ApiCalls.ProductService;

namespace Zebra.Gateway.API.ApiCalls
{
    public static class ConfigureRefit
    {
        public static void ConfiguteRefit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRefitClient<IProductClientFetch>().ConfigureHttpClient(c =>
                c.BaseAddress = new Uri(configuration["Apis:ProductService"]));

            services.AddRefitClient<IProductManagementFetch>().ConfigureHttpClient(c =>
                c.BaseAddress = new Uri(configuration["Apis:ProductService"]));
        }
    }
}
