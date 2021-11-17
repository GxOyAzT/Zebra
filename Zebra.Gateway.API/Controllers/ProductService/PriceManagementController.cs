using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Zebra.Gateway.API.ApiCalls.ProductService;
using Zebra.Gateway.API.ApiCalls.ProductService.Commands;
using Zebra.Shared.LoggerDriver.Services.Interfaces;

namespace Zebra.Gateway.API.Controllers.ProductService
{
    [ApiController]
    [Route("[controller]")]
    public class PriceManagementController : ControllerBase
    {
        private readonly IMessageLogger _messageLogger;
        private readonly IPriceManagementFetch _priceManagementFetch;

        public PriceManagementController(IMessageLogger messageLogger, IPriceManagementFetch priceManagementFetch)
        {
            _messageLogger = messageLogger;
            _priceManagementFetch = priceManagementFetch;
        }

        [HttpDelete]
        [Route("deletePrice")]
        public async Task<IActionResult> DeletePrice([FromBody] DeletePriceCommand deletePriceCommand)
        {
            try
            {
                await _priceManagementFetch.DeletePrice(deletePriceCommand);
                return Ok();
            }
            catch (HttpRequestException ex)
            {
                _messageLogger.Log("Cannot fetch IPriceManagementFetch", Shared.LoggerDriver.Domain.Enums.LogTypeEnum.Information);
                return StatusCode(417);
            }
        }

        [HttpPost]
        [Route("updateproductprice")]
        public async Task<IActionResult> UpdateProductPrice([FromBody] AddPriceCommand addPriceCommand)
        {
            try
            {
                await _priceManagementFetch.UpdatePrice(addPriceCommand);
                return Ok();
            }
            catch (HttpRequestException ex)
            {
                _messageLogger.Log("Cannot fetch IPriceManagementFetch", Shared.LoggerDriver.Domain.Enums.LogTypeEnum.Information);
                return StatusCode(417);
            }
            catch (ApiException ex)
            {
                _messageLogger.Log(ex.Content, Shared.LoggerDriver.Domain.Enums.LogTypeEnum.Information);
                return StatusCode((int)ex.StatusCode, ex.Content);
            }
        }
    }
}
