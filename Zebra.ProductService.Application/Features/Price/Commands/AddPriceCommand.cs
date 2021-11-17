using MediatR;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Zebra.ProductService.Application.Features.Price.Queries;
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
                throw new IncorrectInputFormatException($"Tax has to be intiger from 1 to 100. Actual: {request.Tax}", HttpStatusCode.BadRequest);
            }

            if (request.Cost <= 0)
            {
                throw new IncorrectInputFormatException($"Price cannot be lower then 0. Actual: {request.Cost}", HttpStatusCode.BadRequest);
            }

            if (request.From < DateTime.Now.Date.AddDays(1))
            {
                throw new IncorrectInputFormatException($"Price premiere date cannot be earlier then next day. Actual: {request.From}", HttpStatusCode.BadRequest);
            }

            var getAllProductPricesQuery = new GetAllProductPricesQuery(request.ProductId);

            var pricesForProduct = await _mediator.Send(getAllProductPricesQuery);

            var priceWhereDate = pricesForProduct.FirstOrDefault(e => e.From == request.From.Date);

            if (priceWhereDate != null)
            {
                throw new DomainRulesException($"Premiere price of date {request.From.Date.ToString("dd-MM-yyyy")} already exists.", HttpStatusCode.BadRequest);
            }

            var price = new PriceModel()
            {
                ProductId = request.ProductId,
                Cost = request.Cost,
                Tax = request.Tax,
                From = request.From.Date
            };

            await _priceRepository.Insert(price);

            return Unit.Value;
        }
    }
}
