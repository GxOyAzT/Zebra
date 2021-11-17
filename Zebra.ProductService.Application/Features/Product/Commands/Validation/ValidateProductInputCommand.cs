using MediatR;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Zebra.ProductService.Application.Features.Product.Queries;
using Zebra.ProductService.Domain.Exceptions;

namespace Zebra.ProductService.Application.Features.Product.Commands.Validation
{
    public sealed record ValidateProductInputCommand(Guid Id, string Name, string Description, string Ean) : IRequest;

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

            if (String.IsNullOrEmpty(request.Name))
            {
                throw new IncorrectInputFormatException("Product name cannot be empty value.");
            }

            if (existingProducts.Where(e => e.Id != request.Id).Select(e => e.Name.ToLower()).Contains(request.Name.ToLower()))
            {
                throw new IncorrectInputFormatException("product of this name already exists.");
            }

            if (request.Description.Length > 100)
            {
                throw new IncorrectInputFormatException("Description cannot be longer then 100 characters.");
            }

            if (String.IsNullOrEmpty(request.Ean))
            {
                throw new IncorrectInputFormatException("Ean has to be passed.");
            }

            if (!new Regex("^[0-9]{11}$").IsMatch(request.Ean))
            {
                throw new IncorrectInputFormatException("Ean has to be 11 length digitals string.");
            }

            return Unit.Value;
        }
    }
}
