using System;

namespace Zebra.Gateway.API.ApiCalls.ProductService.Commands
{
    public record UpdateProductCommand(Guid Id, string Name, string Description, bool IsInSale);
}
