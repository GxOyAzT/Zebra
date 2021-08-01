using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zebra.ProductService.Application.Features.Product.Queries;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Domain.Exceptions;
using Zebra.ProductService.Persistance.Repository.Price;

namespace Zebra.ProductService.Application.Features.Price.Commands
{
    public sealed record AddPriceCommand(Guid ProductId, int Tax, decimal Cost, DateTime From) : IRequest;

    public sealed class AddPriceCommandHandler : IRequestHandler<AddPriceCommand, Unit>
    {
        private readonly IMediator _mediator;
        private readonly IPriceRepository _priceRepository;

        public AddPriceCommandHandler(
            IMediator mediator,
            IPriceRepository priceRepository)
        {
            _mediator = mediator;
            _priceRepository = priceRepository;
        }

        public async Task<Unit> Handle(AddPriceCommand request, CancellationToken cancellationToken)
        {
            var isProductExistsRequest = new GetProductQuery(request.ProductId);
            await _mediator.Send(isProductExistsRequest);

            if (request.Tax < 1 && request.Tax > 99)
            {
                throw new IncorrectInputFormatException($"Tax has to be intiger from 1 to 100. Actual: {request.Tax}");
            }

            if (request.Cost <= 0)
            {
                throw new IncorrectInputFormatException($"Price cannot be lower then 0. Actual: {request.Cost}");
            }

            if (request.From < DateTime.Now.Date.AddDays(1))
            {
                throw new IncorrectInputFormatException($"Price premiere date cannot be earlier then next day. Actual: {request.From}");
            }

            var price = new PriceModel()
            {
                ProductModelId = request.ProductId,
                Cost = request.Cost,
                Tax = request.Tax,
                From = request.From
            };

            await _priceRepository.Insert(price);

            return Unit.Value;
        }
    }
}
