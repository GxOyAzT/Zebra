using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zebra.ProductService.Application.Features.Product.Queries;

namespace Zebra.ProductService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("getallavaliableproducts")]
        public async Task<IActionResult> GetAllAvaliableProducts()
        {
            var request = new GetAvaliableProductsQuery();

            var products = await _mediator.Send(request);

            return Ok(products);
        }
    }
}
