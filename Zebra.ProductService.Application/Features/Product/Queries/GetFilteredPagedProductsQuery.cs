using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zebra.ProductService.Domain.ApiModels.Paged;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Domain.Enums;

namespace Zebra.ProductService.Application.Features.Product.Queries
{
    public sealed record GetFilteredPagedProductsQuery(string FilterString, bool IsInSaleFilter, int PageCapacity, int Page) : IRequest<PagedList<ProductModel>>;

    public sealed class GetFilteredPagedProductsQueryHandler : IRequestHandler<GetFilteredPagedProductsQuery, PagedList<ProductModel>>
    {
        private readonly IMediator _mediator;

        public GetFilteredPagedProductsQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<PagedList<ProductModel>> Handle(GetFilteredPagedProductsQuery request, CancellationToken cancellationToken)
        {
            var getAllProductsQuery = new GetAllProductsQuery();

            var products = await _mediator.Send(getAllProductsQuery);

            var filterString = request.FilterString?.ToLower();
            if (!String.IsNullOrEmpty(request.FilterString))
            {
                products = products.Where(e => (String.IsNullOrEmpty(e.Name) ? false : e.Name.ToLower().Contains(filterString)) || (String.IsNullOrEmpty(e.Ean) ? false : e.Ean.Contains(filterString))).ToList();
            }

            if (request.IsInSaleFilter)
            {
                products = products.Where(e => e.IsInSale == request.IsInSaleFilter).ToList();
            }

            return new PagedList<ProductModel>(products, request.PageCapacity, request.Page);
        }
    }
}
