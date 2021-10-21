using Microsoft.Extensions.DependencyInjection;
using Zebra.Shared.FileDriver.Configuration;
using Zebra.Shared.FileDriver.Features.Delete;
using Zebra.Shared.FileDriver.Features.Save;

namespace Zebra.Shared.FileDriver.DependencyInjection
{
    public static class FileManagerDI
    {
        public static IServiceCollection AddFileManager(this IServiceCollection services, string rootPath)
        {
            services.AddTransient<ISaveFile, SaveFile>();
            services.AddTransient<IDeleteFile, DeleteFile>();

            services.AddSingleton(new Options(rootPath));

            return services;
        }
    }
}
