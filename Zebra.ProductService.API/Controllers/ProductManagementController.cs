using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zebra.ProductService.Application.Features.Product.Commands.RequestEntry;
using Zebra.ProductService.Application.Features.Product.Queries;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Domain.Exceptions;
using Zebra.Shared.LoggerDriver.Domain.Enums;
using Zebra.Shared.LoggerDriver.Services.Interfaces;

namespace Zebra.ProductService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductManagementController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMessageLogger _messageLogger;

        public ProductManagementController(
            IMediator mediator,
            IMessageLogger messageLogger)
        {
            _mediator = mediator;
            _messageLogger = messageLogger;
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
                _messageLogger.Log("GetProductQuery is null (ProductManagementController.GetProduct)", LogTypeEnum.Information);
                return BadRequest("Request object cannot be empty.");
            }

            ProductModel productModel;
            try
            {
                productModel = await _mediator.Send(request);
            }
            catch (CannotFindEntityException ex)
            {
                _messageLogger.Log($"{ex.Message} (ProductManagementController.GetProduct)", LogTypeEnum.Information);
                return NotFound(ex.Message);
            }

            return Ok(productModel);
        }

        [HttpPut]
        [Route("updateproduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand request)
        {
            if (request == null)
            {
                _messageLogger.Log("UpdateProductCommand is null (ProductManagementController.UpdateProduct)", LogTypeEnum.Information);
                return BadRequest("Request object cannot be empty.");
            }

            try
            {
                await _mediator.Send(request);
            }
            catch (CannotFindEntityException ex)
            {
                _messageLogger.Log($"{ex.Message} (ProductManagementController.UpdateProduct)", LogTypeEnum.Information);
                return BadRequest(ex.Message);
            }
            catch (IncorrectInputFormatException ex)
            {
                _messageLogger.Log($"{ex.Message} (ProductManagementController.UpdateProduct)", LogTypeEnum.Information);
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPost]
        [Route("addproduct")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductCommand request)
        {
            if (request == null)
            {
                _messageLogger.Log("AddProductCommand is null (ProductManagementController.AddProduct)", LogTypeEnum.Information);
                return BadRequest("Request object cannot be empty.");
            }

            try
            {
                await _mediator.Send(request);
            }
            catch (IncorrectInputFormatException ex)
            {
                _messageLogger.Log($"{ex.Message} (ProductManagementController.AddProduct)", LogTypeEnum.Information);
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
