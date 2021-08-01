using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zebra.ProductService.Application.Features.Product.Commands.RequestEntry;
using Zebra.ProductService.Application.Features.Product.Queries;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Domain.Exceptions;

namespace Zebra.ProductService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductManagementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductManagementController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("getproducts")]
        public async Task<IActionResult> GetProducts()
        {
            var request = new GetAllProductsQuery();

            var products = await _mediator.Send(request);

            return Ok(products);
        }

        [HttpGet]
        [Route("getproduct")]
        public async Task<IActionResult> GetProduct([FromBody] GetProductQuery request)
        {
            if (request == null)
            {
                return BadRequest("Request object cannot be empty.");
            }

            ProductModel productModel;
            try
            {
                productModel = await _mediator.Send(request);
            }
            catch (CannotFindEntityException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok(productModel);
        }

        [HttpPost]
        [Route("updateproduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand request)
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
            catch (IncorrectInputFormatException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
