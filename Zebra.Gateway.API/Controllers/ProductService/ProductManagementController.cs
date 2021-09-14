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
    public class ProductManagementController : ControllerBase
    {
        private readonly IProductManagementFetch _productManagementFetch;
        private readonly IMessageLogger _messageLogger;

        public ProductManagementController(
            IProductManagementFetch productManagementFetch,
            IMessageLogger messageLogger)
        {
            _productManagementFetch = productManagementFetch;
            _messageLogger = messageLogger;
        }

        [HttpGet]
        [Route("getproducts")]
        [Authorize(Policy = "_productPriceManagement")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _productManagementFetch.GetProducts();
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
