using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zebra.ProductService.Domain.Exceptions;
using Zebra.ProductService.Persistance.Repository.Price;

namespace Zebra.ProductService.Application.Features.Price.Commands
{
    public sealed record DeletePriceCommand(Guid Id) : IRequest;

    public sealed class DeletePriceCommandHandler : IRequestHandler<DeletePriceCommand, Unit>
    {
        private readonly IPriceRepository _priceRepository;

        public DeletePriceCommandHandler(IPriceRepository priceRepository)
        {
            _priceRepository = priceRepository;
        }

        public async Task<Unit> Handle(DeletePriceCommand request, CancellationToken cancellationToken)
        {
            var priceForDelete = await _priceRepository.Get(request.Id);

            if (priceForDelete == null)
            {
                throw new CannotFindEntityException($"Cannot find price of ID: {request.Id}");
            }

            if (priceForDelete.From < DateTime.Now.Date.AddDays(1))
            {
                throw new DomainRulesException($"You cannot price which premiere date is lower then next day. Actual premiere date for price you want to delete: {priceForDelete.From}");
            }

            await _priceRepository.Delete(priceForDelete);

            return Unit.Value;
        }
    }
}
