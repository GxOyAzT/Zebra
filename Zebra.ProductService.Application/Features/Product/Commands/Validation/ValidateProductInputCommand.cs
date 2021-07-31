using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zebra.ProductService.Application.Features.Product.Queries;
using Zebra.ProductService.Domain.Exceptions;

namespace Zebra.ProductService.Application.Features.Product.Commands.Validation
{
    public sealed record ValidateProductInputCommand(string Name, string Description) : IRequest;

    public sealed class ValidateProductInputCommandHandler : IRequestHandler<ValidateProductInputCommand, Unit>
    {
        private readonly IMediator _mediator;

        public ValidateProductInputCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Unit> Handle(ValidateProductInputCommand request, CancellationToken cancellationToken)
        {
            var existingProducts = await _mediator.Send(new GetAllProductsQuery());

            if (existingProducts.Select(e => e.Name.ToLower()).Contains(request.Name.ToLower()))
            {
                throw new IncorrectInputFormatException("Request already exists.");
            }

            if (request.Description.Length > 100)
            {
                throw new IncorrectInputFormatException("Description cannot be longer then 100 characters.");
            }

            return Unit.Value;
        }
    }
}
