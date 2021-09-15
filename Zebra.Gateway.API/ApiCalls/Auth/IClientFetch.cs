using Refit;
using System.Threading.Tasks;
using Zebra.Gateway.ApiModels.Auth;

namespace Zebra.Gateway.API.ApiCalls.Auth
{
    public interface IClientFetch
    {
        [Post("/api/client/login")]
        Task<string> Login([Body] RegisterLoginClientApiModel model, [Header("Accept-Language")] string lang);

        [Post("/api/client/register")]
        Task<string> Register([Body] RegisterLoginClientApiModel model, [Header("Accept-Language")] string lang);
    }
}
