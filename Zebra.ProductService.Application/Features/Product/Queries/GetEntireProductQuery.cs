using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zebra.ProductService.Application.Features.Price.Queries;
using Zebra.ProductService.Application.Features.Rating.Queries;
using Zebra.ProductService.Domain.ApiModels.Product;

namespace Zebra.ProductService.Application.Features.Product.Queries
{
    public sealed record GetEntireProductQuery(Guid ProductId) : IRequest<ProductEntireApiModel>;

    public sealed class GetEntireProductQueryHandler : IRequestHandler<GetEntireProductQuery, ProductEntireApiModel>
    {
        private readonly IMediator _mediator;

        public GetEntireProductQueryHandler(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ProductEntireApiModel> Handle(GetEntireProductQuery request, CancellationToken cancellationToken)
        {
            var getProductQuery = new GetProductQuery(request.ProductId);
            var product = await _mediator.Send(getProductQuery);

            var getAllProductPricesQuery = new GetAllProductPricesQuery(request.ProductId);
            var prices = await _mediator.Send(getAllProductPricesQuery);

            var getRatingsForProductQuery = new GetRatingsForProductQuery(request.ProductId);
            var ratings = await _mediator.Send(getRatingsForProductQuery);

            var output = new ProductEntireApiModel();

            output.Product = product;

            output.Prices = prices;

            output.Ratings = ratings;

            return output;
        }
    }
}
