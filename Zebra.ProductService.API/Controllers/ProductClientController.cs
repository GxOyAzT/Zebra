using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zebra.ProductService.Application.Features.ProductPrice.Query;

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
        [Route("getallavaliableproducts/{pageCapacity}/{page}")]
        public async Task<IActionResult> GetAllAvaliableProducts(int pageCapacity, int page)
        {
            var request = new GetProductsPriceQuery(pageCapacity, page);

            var products = await _mediator.Send(request);

            return Ok(products);
        }
    }
}
