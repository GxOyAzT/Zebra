using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Zebra.ProductService.Application;
using Zebra.ProductService.Persistance.Context;
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
            services.AddDbContext<ApplicationDbContext>(conf =>
            {
                conf.UseSqlServer(Configuration.GetConnectionString("Zebra_Product"));
            });

            services.AddCors(policy =>
                policy.AddPolicy("OpenCorsPolicy", options => options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()));

            services.AddMediatR(typeof(MediaREntryPoint));

            services.ConfigureLoggerDriver("ProductService");

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPriceRepository, PriceRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Zebra.ProductService.API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zebra.ProductService.API"));
            }

            app.UseHttpsRedirection();

            app.UseCors("OpenCorsPolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}