using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Zebra.ProductService.Application.Features.Product.Commands.RequestEntry;
using Zebra.ProductService.Application.Features.Product.Queries;
using Zebra.ProductService.Domain.ApiModels.Product;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Domain.Enums;
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
        [Route("getfilteredpagedproducts")]
        public async Task<IActionResult> GetFilteredPagedProducts(string filterString, bool isInSaleFilterEnum, int pageCapacity, int page)
        {
            var request = new GetFilteredPagedProductsQuery(filterString, isInSaleFilterEnum, pageCapacity, page);

            var products = await _mediator.Send(request);

            return Ok(products);
        }

        [HttpGet]
        [Route("getproduct/{productId}")]
        public async Task<IActionResult> GetProduct(Guid productId)
        {
            var getProductQuery = new GetProductQuery(productId);

            var productModel = await _mediator.Send(getProductQuery);

            return Ok(productModel);
        }

        [HttpGet]
        [Route("getentireproduct/{productId}")]
        public async Task<IActionResult> GetEntireProduct(Guid productId)
        {
            var getEntireProductQuery = new GetEntireProductQuery(productId);

            var productModel = await _mediator.Send(getEntireProductQuery);

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

            await _mediator.Send(request);

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

            await _mediator.Send(request);

            return Ok();
        }
    }
}
