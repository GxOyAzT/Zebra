using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zebra.ProductService.Application.ApiModels.Product;
using Zebra.ProductService.Application.Features.Price.Queries;
using Zebra.ProductService.Domain.Exceptions;

namespace Zebra.ProductService.Application.Features.Product.Queries
{
    public sealed record GetAvaliableProductsQuery() : IRequest<List<ProductPriceApiModel>>;
    public sealed class GetAvaliableProductsQueryHandler : IRequestHandler<GetAvaliableProductsQuery, List<ProductPriceApiModel>>
    {
        private readonly IMediator _mediator;

        public GetAvaliableProductsQueryHandler(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<List<ProductPriceApiModel>> Handle(GetAvaliableProductsQuery request, CancellationToken cancellationToken)
        {
            var getAllProductsQuery = new GetAllProductsQuery();
            var products = await _mediator.Send(getAllProductsQuery);

            List<ProductPriceApiModel> output = new();
            foreach(var product in products.Where(e => e.IsInSale))
            {
                var getActualPriceQuery = new GetActualPriceQuery(product.Id);

                try
                {
                    var priceForProduct = await _mediator.Send(getActualPriceQuery);

                    output.Add(new ProductPriceApiModel()
                    {
                        ProductName = product.Name,
                        Cost = priceForProduct.Cost
                    });
                }
                catch (Exception ex)
                {
                    if (ex is CollectionIsEmptyException || ex is CannotFindEntityException)
                    {
                        continue;
                    }

                    throw;
                }
            }

            return output;
        }
    }
}
