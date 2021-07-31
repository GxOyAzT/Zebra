using System;

namespace Zebra.ProductService.Domain.Shared
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
    }
}
