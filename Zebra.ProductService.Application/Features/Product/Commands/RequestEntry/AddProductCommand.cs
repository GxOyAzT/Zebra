using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zebra.ProductService.Application.Features.Files;
using Zebra.ProductService.Application.Features.Product.Commands.Validation;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Persistance.Repository.Product;

namespace Zebra.ProductService.Application.Features.Product.Commands.RequestEntry
{
    public sealed record AddProductCommand(string Name, string Description, string ImageSrc) : IRequest<Guid>;

    public sealed class AddProductCommandHandler : IRequestHandler<AddProductCommand, Guid>
    {
        private readonly IMediator _mediator;
        private readonly IProductRepository _productRepository;
        private readonly IRelativeFilePathResolver _relativeFilePathResolver;

        public AddProductCommandHandler(
            IMediator mediator,
            IProductRepository productRepository,
            IRelativeFilePathResolver relativeFilePathResolver)
        {
            _mediator = mediator;
            _productRepository = productRepository;
            _relativeFilePathResolver = relativeFilePathResolver;
        }

        public async Task<Guid> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Send(new ValidateProductInputCommand(request.Name, request.Description));

            var product = new ProductModel()
            {
                Name = request.Name,
                Description = request.Description,
                IsInSale = false,
                AddDate = DateTime.Now
            };

            var resut = await _productRepository.Insert(product);

            await _mediator.Send(new SaveFileCommand(request.ImageSrc, _relativeFilePathResolver.ProductImages, $"product{resut}"));

            return resut;
        }
    }
}