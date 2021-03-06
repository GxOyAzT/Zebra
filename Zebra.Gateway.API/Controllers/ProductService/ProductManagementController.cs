using AspAuth = Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Zebra.Gateway.API.ApiCalls.ProductService;
using Zebra.Shared.LoggerDriver.Services.Interfaces;
using Zebra.Gateway.API.ApiCalls.ProductService.Commands;

namespace Zebra.Gateway.API.Controllers.ProductService
{
    [AspAuth.Authorize(Policy = "_productPriceManagement")]
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
        public async Task<IActionResult> GetProducts([FromHeader(Name = "Accept-Language")] string lang)
        {
            try
            {
                var products = await _productManagementFetch.GetProducts(lang);
                return Ok(products);
            }
            catch (HttpRequestException ex)
            {
                _messageLogger.Log("Cannot fetch IProductClientFetch", Shared.LoggerDriver.Domain.Enums.LogTypeEnum.Information);
                return StatusCode(417);
            }
        }

        [HttpGet]
        [Route("getproduct/{productId}")]
        public async Task<IActionResult> GetProduct(Guid productId, [FromHeader(Name = "Accept-Language")] string lang)
        {
            try
            {
                var products = await _productManagementFetch.GetProduct(productId, lang);
                return Ok(products);
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

        [HttpGet]
        [Route("getentireproduct/{productId}")]
        public async Task<IActionResult> GetEntireProduct(Guid productId, [FromHeader(Name = "Accept-Language")] string lang)
        {
            try
            {
                var product = await _productManagementFetch.GetEntireProduct(productId, lang);
                return Ok(product);
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

        [HttpPut]
        [Route("updateproduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand command, [FromHeader(Name = "Accept-Language")] string lang)
        {
            try
            {
                await _productManagementFetch.UpdateProduct(command, lang);
                return Ok();
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

        [HttpPost]
        [Route("addproduct")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductCommand command, [FromHeader(Name = "Accept-Language")] string lang)
        {
            try
            {
                await _productManagementFetch.AddProduct(command, lang);
                return Ok();
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
