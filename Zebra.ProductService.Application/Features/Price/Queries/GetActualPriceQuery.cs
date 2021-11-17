using MediatR;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Zebra.ProductService.Application.Features.Product.Queries;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Domain.Exceptions;

namespace Zebra.ProductService.Application.Features.Price.Queries
{
    public sealed record GetActualPriceQuery(Guid ProductId) : IRequest<PriceModel>;

    public sealed record GetActualPriceQueryHandler : IRequestHandler<GetActualPriceQuery, PriceModel>
    {
        private readonly IMediator _mediator;

        public GetActualPriceQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<PriceModel> Handle(GetActualPriceQuery request, CancellationToken cancellationToken)
        {
            var isProductExistsRequest = new GetProductQuery(request.ProductId);
            await _mediator.Send(isProductExistsRequest);

            var allProductPricesRequest = new GetAllProductPricesQuery(request.ProductId);
            var allProductPrices = await _mediator.Send(allProductPricesRequest);

            if (!allProductPrices.Any())
            {
                throw new CollectionIsEmptyException($"There is no price related to product of ID: {request.ProductId}", HttpStatusCode.BadRequest);
            }

            var actualPrice = allProductPrices.Where(e => e.From < DateTime.Now)
                .OrderByDescending(e => e.From)
                .FirstOrDefault();

            if (actualPrice == null)
            {
                throw new CannotFindEntityException("Cannot find actual price.", HttpStatusCode.NotFound);
            }

            return actualPrice;
        }
    }
}
