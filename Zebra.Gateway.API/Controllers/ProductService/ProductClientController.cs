using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Zebra.Gateway.API.ApiCalls.ProductService;
using Zebra.Shared.LoggerDriver.Services.Interfaces;

namespace Zebra.Gateway.API.Controllers.ProductService
{
    [ApiController]
    [Route("[controller]")]
    public class ProductClientController : ControllerBase
    {
        private readonly IProductClientFetch _productClientFetch;
        private readonly IMessageLogger _messageLogger;

        public ProductClientController(
            IProductClientFetch productClientFetch,
            IMessageLogger messageLogger)
        {
            _productClientFetch = productClientFetch;
            _messageLogger = messageLogger;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("getallavaliableproducts")]
        public async Task<IActionResult> GetAllAvaliableProducts()
        {
            try
            {
                var products = await _productClientFetch.GetAllAvaliableProducts();
                return Ok(products);
            }
            catch (HttpRequestException ex)
            {
                _messageLogger.Log("Cannot fetch IProductClientFetch", Shared.LoggerDriver.Domain.Enums.LogTypeEnum.Information);
                return StatusCode(417);
            }
        }
    }
}
