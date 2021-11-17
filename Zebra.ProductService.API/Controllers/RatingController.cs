using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zebra.ProductService.Application.Features.Rating.Commands;
using Zebra.ProductService.Domain.Exceptions;
using Zebra.Shared.LoggerDriver.Domain.Enums;
using Zebra.Shared.LoggerDriver.Services.Interfaces;

namespace Zebra.ProductService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMessageLogger _messageLogger;

        public RatingController(
            IMediator mediator,
            IMessageLogger messageLogger)
        {
            _mediator = mediator;
            _messageLogger = messageLogger;
        }

        [HttpPost]
        [Route("addrating")]
        public async Task<IActionResult> AddRating([FromBody] AddRatingCommand command)
        {
            if (command == null)
            {
                _messageLogger.Log($"AddRatingCommand is null (RatingController.AddRating)", LogTypeEnum.Information);
                return BadRequest("Request object cannot be empty.");
            }

            await _mediator.Send(command);

            return Ok();
        } 
    }
}
