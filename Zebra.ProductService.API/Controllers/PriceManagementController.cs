using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zebra.ProductService.Application.Features.Price.Commands;
using Zebra.ProductService.Application.Features.Price.Queries;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Domain.Exceptions;
using Zebra.Shared.LoggerDriver.Domain.Enums;
using Zebra.Shared.LoggerDriver.Services.Interfaces;

namespace Zebra.ProductService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PriceManagementController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMessageLogger _messageLogger;

        public PriceManagementController(
            IMediator mediator,
            IMessageLogger messageLogger)
        {
            _mediator = mediator;
            _messageLogger = messageLogger;
        }

        [HttpGet]
        [Route("getactualproductprice")]
        public async Task<IActionResult> GetActualProductPrice([FromBody] GetActualPriceQuery request)
        {
            if (request == null)
            {
                _messageLogger.Log($"GetActualPriceQuery is null (PriceManagementController.GetActualProductPrice)", LogTypeEnum.Information);
                return BadRequest("Request object cannot be empty.");
            }

            var priceModel = await _mediator.Send(request);

            return Ok(priceModel);
        }

        [HttpPost]
        [Route("updateproductprice")]
        public async Task<IActionResult> UpdateProductPrice([FromBody] AddPriceCommand request)
        {
            if (request == null)
            {
                _messageLogger.Log($"AddPriceCommand is null (PriceManagementController.UpdateProductPrice)", LogTypeEnum.Information);
                return BadRequest("Request object cannot be empty.");
            }

            await _mediator.Send(request);

            return Ok();
        }

        [HttpDelete]
        [Route("deleteprice")]
        public async Task<IActionResult> DeletePrice([FromBody] DeletePriceCommand request)
        {
            if (request == null)
            {
                _messageLogger.Log($"DeletePriceCommand is null (PriceManagementController.DeletePrice)", LogTypeEnum.Information);
                return BadRequest("Request object cannot be empty.");
            }

            await _mediator.Send(request);

            return Ok();
        }
    }
}
