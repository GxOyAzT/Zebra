using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zebra.ProductService.Application;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Persistance.Repository.Price;
using Zebra.ProductService.Persistance.Repository.Product;
using Zebra.ProductService.Persistance.Repository.Rating;
using Zebra.Shared.LoggerDriver.DIConfiguration;

namespace Zebra.ProductService.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(MediaREntryPoint));

            services.ConfigureLoggerDriver("ProductService");

            services.AddScoped<IProductRepository, MockProductRepo>();
            services.AddScoped<IPriceRepository, MockPriceRepo>();
            services.AddScoped<IRatingRepository, MockRatingRepo>();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public class MockProductRepo : IProductRepository
    {
        public Task Delete(ProductModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<ProductModel> Get(Guid id)
        {
            return Task.FromResult(new ProductModel()
            {
                Id = new Guid(),
                IsInSale = true,
                Name = "Name",
                AddDate = DateTime.Now.AddDays(-200),
                Description = "Description"
            });
        }

        public Task<List<ProductModel>> GetAll()
        {
            return Task.FromResult(new List<ProductModel>()
            {
                new ProductModel()
                {
                    Id = new Guid("4be0c6d2-bd29-4fa7-8002-a5f10a7c9686"),
                    IsInSale = true,
                    Name = "Name",
                    AddDate = DateTime.Now.AddDays(-200),
                    Description = "Description"
                }   
            });
        }

        public Task Insert(ProductModel entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(ProductModel entity)
        {
            throw new NotImplementedException();
        }
    }

    public class MockPriceRepo : IPriceRepository
    {
        public Task Delete(PriceModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<PriceModel> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PriceModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Insert(PriceModel entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(PriceModel entity)
        {
            throw new NotImplementedException();
        }
    }

    public class MockRatingRepo : IRatingRepository
    {
        public Task Delete(RatingModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<RatingModel> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<RatingModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Insert(RatingModel entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(RatingModel entity)
        {
            throw new NotImplementedException();
        }
    }
}