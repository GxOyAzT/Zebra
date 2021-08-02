using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zebra.ProductService.Application.Features.Price.Commands;
using Zebra.ProductService.Application.Features.Price.Queries;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Domain.Exceptions;

namespace Zebra.ProductService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PriceManagementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PriceManagementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("getactualproductprice")]
        public async Task<IActionResult> GetActualProductPrice([FromBody] GetActualPriceQuery request)
        {
            if (request == null)
            {
                return BadRequest("Request object cannot be empty.");
            }

            PriceModel priceModel;
            try
            {
                priceModel = await _mediator.Send(request);
            }
            catch (CannotFindEntityException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (CollectionIsEmptyException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(priceModel);
        }

        [HttpPost]
        [Route("updateproductprice")]
        public async Task<IActionResult> UpdateProductPrice([FromBody] AddPriceCommand request)
        {
            if (request == null)
            {
                return BadRequest("Request object cannot be empty.");
            }

            try
            {
                await _mediator.Send(request);
            }
            catch (CannotFindEntityException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (CollectionIsEmptyException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("deleteprice")]
        public async Task<IActionResult> DeletePrice([FromBody] DeletePriceCommand request)
        {
            if (request == null)
            {
                return BadRequest("Request object cannot be empty.");
            }

            try
            {
                await _mediator.Send(request);
            }
            catch (CannotFindEntityException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (CollectionIsEmptyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DomainRulesException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
