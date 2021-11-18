using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Zebra.ProductService.Domain.ApiModels.Paged;
using Zebra.ProductService.Domain.Views;
using Zebra.ProductService.Persistance.Repository.ProductPriceView;

namespace Zebra.ProductService.Application.Features.ProductPrice.Query
{
    public sealed record GetProductsPriceQuery(int PageCapacity, int Page) : IRequest<PagedList<ProductPriceModel>>;

    public sealed class GetProductsPriceQueryHandler : IRequestHandler<GetProductsPriceQuery, PagedList<ProductPriceModel>>
    {
        private readonly IProductPriceRepo _productPriceRepo;

        public GetProductsPriceQueryHandler(IProductPriceRepo productPriceRepo)
        {
            _productPriceRepo = productPriceRepo;
        }

        public async Task<PagedList<ProductPriceModel>> Handle(GetProductsPriceQuery request, CancellationToken cancellationToken)
        {
            return new PagedList<ProductPriceModel>(await _productPriceRepo.GetProductPrices(), request.PageCapacity, request.Page);
        }
    }

}
