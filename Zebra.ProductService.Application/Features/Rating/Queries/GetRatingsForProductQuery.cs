using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Persistance.Repository.Rating;

namespace Zebra.ProductService.Application.Features.Rating.Queries
{
    public sealed record GetRatingsForProductQuery(Guid productId) : IRequest<List<RatingModel>>;

    public sealed class GetRatingsForProductQueryHandler : IRequestHandler<GetRatingsForProductQuery, List<RatingModel>>
    {
        private readonly IRatingRepository _ratingRepository;

        public GetRatingsForProductQueryHandler(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task<List<RatingModel>> Handle(GetRatingsForProductQuery request, CancellationToken cancellationToken)
        {
            var allRatings = await _ratingRepository.GetAll();
            return allRatings.Where(e => e.ProductId == request.productId).ToList();
        }
    }
}
