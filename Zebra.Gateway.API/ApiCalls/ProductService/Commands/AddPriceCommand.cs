using System;

namespace Zebra.Gateway.API.ApiCalls.ProductService.Commands
{
    public sealed record AddPriceCommand(Guid ProductId, int Tax, decimal Cost, DateTime From);
}
