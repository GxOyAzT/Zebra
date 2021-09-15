﻿using AspAuth = Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Zebra.Gateway.API.ApiCalls.ProductService;
using Zebra.Gateway.API.ApiCalls.ProductService.Queries;
using Zebra.Shared.LoggerDriver.Services.Interfaces;

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

        [HttpGet]
        [Route("getproduct/{productId}")]
        public async Task<IActionResult> GetProduct(Guid productId)
        {
            try
            {
                var query = new GetProductQuery(productId);
                var products = await _productManagementFetch.GetProduct(query);
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
    }
}