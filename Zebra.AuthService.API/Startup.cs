using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Zebra.AuthService.API.HostedServices;
using Zebra.AuthService.API.Models;
using Zebra.AuthService.API.Persistance;
using Zebra.AuthService.API.Services.RabbitModel;
using Zebra.AuthService.API.Services.Token;
using Zebra.Shared.LoggerDriver.DIConfiguration;

namespace Zebra.AuthService.API
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
            services.AddDbContext<AuthDbContext>(conf =>
            {
                conf.UseSqlServer(Configuration.GetConnectionString("Zebra_Auth"));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(conf =>
            {
                conf.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AuthDbContext>();

            services.ConfigureLoggerDriver("AuthService");

            services.AddScoped<ICreateToken, CreateToken>();

            services.AddControllers();

            services.AddControllers()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            services.Configure<RequestLocalizationOptions>(
                opt =>
                {
                    opt.DefaultRequestCulture = new RequestCulture("en");

                    opt.SupportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en"),
                        new CultureInfo("pl")
                    };

                    opt.SupportedUICultures = new List<CultureInfo>
                    {
                        new CultureInfo("en"),
                        new CultureInfo("pl")
                    };
                });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Zebra.AuthService.API", Version = "v1" });
            });

            services.AddSingleton<ICreateModel, CreateModel>();

            services.AddHostedService<NewCustomerReceiver>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zebra.AuthService.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
