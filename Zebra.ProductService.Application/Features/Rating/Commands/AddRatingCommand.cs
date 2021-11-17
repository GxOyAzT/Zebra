using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Zebra.ProductService.Application.Features.Product.Queries;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Domain.Exceptions;
using Zebra.ProductService.Persistance.Repository.Rating;

namespace Zebra.ProductService.Application.Features.Rating.Commands
{
    public sealed record AddRatingCommand(int Score, string Review, Guid ProductId) : IRequest;

    public sealed class AddRatingCommandHandler : IRequestHandler<AddRatingCommand>
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IMediator _mediator;

        public AddRatingCommandHandler(
            IRatingRepository ratingRepository,
            IMediator mediator)
        {
            _ratingRepository = ratingRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(AddRatingCommand request, CancellationToken cancellationToken)
        {
            var getProductQuery = new GetProductQuery(request.ProductId);
            await _mediator.Send(getProductQuery);

            if (request.Score > 5 || request.Score < 1)
            {
                throw new IncorrectInputFormatException($"Score cannot be lower then 1 and higher then 5. Actual value: {request.Score}", HttpStatusCode.BadRequest);
            }

            if (request.Review.Length > 200)
            {
                throw new IncorrectInputFormatException($"Review cannot be longer then 200 characters. Actual: {request.Review.Length} characters.", HttpStatusCode.BadRequest);
            }

            var ratingModel = new RatingModel()
            {
                ProductId = request.ProductId,
                Score = request.Score,
                Review = request.Review,
                AddDate = DateTime.Now
            };

            await _ratingRepository.Insert(ratingModel);

            return Unit.Value;
        }
    }
}
