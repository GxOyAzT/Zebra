using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Persistance.Repository.Product;

namespace Zebra.ProductService.Application.Features.Product.Queries
{
    public sealed record GetAllProductsQuery() : IRequest<List<ProductModel>>;

    public sealed class GetAllProductsQueryHanlder : IRequestHandler<GetAllProductsQuery, List<ProductModel>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHanlder(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductModel>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetAll();
        }
    }
}
