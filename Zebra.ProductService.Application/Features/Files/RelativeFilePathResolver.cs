using Microsoft.Extensions.Configuration;

namespace Zebra.ProductService.Application.Features.Files
{
    public class RelativeFilePathResolver : IRelativeFilePathResolver
    {
        private readonly IConfiguration configuration;

        public RelativeFilePathResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string ProductImages => configuration["Files:ProductImages"];
    }
}
