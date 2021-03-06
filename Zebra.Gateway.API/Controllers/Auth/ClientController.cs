using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Net.Http;
using System.Threading.Tasks;
using Zebra.Gateway.API.ApiCalls.Auth;
using Zebra.Gateway.ApiModels.Auth;
using Zebra.Shared.LoggerDriver.Services.Interfaces;

namespace Zebra.Gateway.API.Controllers.Auth
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientFetch _clientFetch;
        private readonly ILoginFetch _loginFetch;
        private readonly IMessageLogger _messageLogger;

        public ClientController(
            IClientFetch clientFetch,
            ILoginFetch loginFetch,
            IMessageLogger messageLogger)
        {
            _clientFetch = clientFetch;
            _loginFetch = loginFetch;
            _messageLogger = messageLogger;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] RegisterLoginClientApiModel model, [FromHeader(Name = "Accept-Language")] string lang)
        {
            try
            {
                var token = await _loginFetch.Login(model, lang);
                return Ok(token);
            }
            catch (ApiException ex)
            {
                _messageLogger.Log(ex.Content, Shared.LoggerDriver.Domain.Enums.LogTypeEnum.Information);
                return StatusCode((int)ex.StatusCode, ex.Content);
            }
            catch (HttpRequestException ex)
            {
                _messageLogger.Log("Cannot fetch IProductClientFetch", Shared.LoggerDriver.Domain.Enums.LogTypeEnum.Information);
                return StatusCode(417);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterLoginClientApiModel model, [FromHeader(Name = "Accept-Language")] string lang)
        {
            try
            {
                var token = await _clientFetch.Register(model, lang);
                return Ok(token);
            }
            catch (ApiException ex)
            {
                _messageLogger.Log(ex.Content, Shared.LoggerDriver.Domain.Enums.LogTypeEnum.Information);
                return StatusCode((int)ex.StatusCode, ex.Content);
            }
            catch (HttpRequestException ex)
            {
                _messageLogger.Log("Cannot fetch IProductClientFetch", Shared.LoggerDriver.Domain.Enums.LogTypeEnum.Information);
                return StatusCode(417);
            }
        }
    }
}
