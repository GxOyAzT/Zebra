using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Persistance.Repository.Price;

namespace Zebra.ProductService.Application.Features.Price.Queries
{
    public sealed record GetAllProductPricesQuery(Guid ProductId) : IRequest<List<PriceModel>>;

    public sealed class GetAllPricesQueryHandler : IRequestHandler<GetAllProductPricesQuery, List<PriceModel>>
    {
        private readonly IPriceRepository _priceRepository;

        public GetAllPricesQueryHandler(IPriceRepository priceRepository)
        {
            _priceRepository = priceRepository;
        }

        public async Task<List<PriceModel>> Handle(GetAllProductPricesQuery request, CancellationToken cancellationToken)
        {
            return (await _priceRepository.GetAll()).Where(e => e.ProductId == request.ProductId).OrderByDescending(e => e.From).ToList();
        }
    }
}
