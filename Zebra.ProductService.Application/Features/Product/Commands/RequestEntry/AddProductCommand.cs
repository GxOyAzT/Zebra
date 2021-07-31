using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zebra.ProductService.Application.Features.Product.Commands.Validation;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Persistance.Repository.Product;

namespace Zebra.ProductService.Application.Features.Product.Commands.RequestEntry
{
    public sealed record AddProductCommand(string Name, string Description) : IRequest;

    public sealed class AddProductCommandHandler : IRequestHandler<AddProductCommand, Unit>
    {
        private readonly IMediator _mediator;
        private readonly IProductRepository _productRepository;

        public AddProductCommandHandler(
            IMediator mediator,
            IProductRepository productRepository)
        {
            _mediator = mediator;
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Send(new ValidateProductInputCommand(request.Name, request.Description));

            var product = new ProductModel()
            {
                Name = request.Name,
                Description = request.Description,
                IsInSale = false,
                AddDate = DateTime.Now
            };

            await _productRepository.Insert(product);

            return Unit.Value;
        }
    }
}