using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zebra.ProductService.Application.Features.Product.Commands.Validation;
using Zebra.ProductService.Application.Features.Product.Queries;
using Zebra.ProductService.Persistance.Repository.Product;

namespace Zebra.ProductService.Application.Features.Product.Commands.RequestEntry
{
    public record UpdateProductCommand(Guid Id, string Name, string Description, bool IsInSale, string Ean) : IRequest;

    public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IMediator _mediator;
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(
            IMediator mediator,
            IProductRepository productRepository)
        {
            _mediator = mediator;
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var getProductRequest = new GetProductQuery(request.Id);
            var product = await _mediator.Send(getProductRequest);

            await _mediator.Send(new ValidateProductInputCommand(request.Name, request.Description, request.Ean));

            product.Name = request.Name;
            product.Description = request.Description;
            product.IsInSale = request.IsInSale;
            product.Ean = request.Ean;

            await _productRepository.Update(product);

            return Unit.Value;
        }
    }
}
