using MediatR;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Domain.Exceptions;

namespace Zebra.ProductService.Application.Features.Product.Queries
{
    public sealed record GetProductQuery(Guid Id) : IRequest<ProductModel>;

    public sealed class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductModel>
    {
        private readonly IMediator _mediator;

        public GetProductQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ProductModel> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var existingProducts = await _mediator.Send(new GetAllProductsQuery());

            var searchProduct = existingProducts.FirstOrDefault(e => e.Id == request.Id);

            if (searchProduct == null)
            {
                throw new CannotFindEntityException($"Cannot find Product of id {request.Id}", HttpStatusCode.NotFound);
            }

            return searchProduct;
        }
    }
}
