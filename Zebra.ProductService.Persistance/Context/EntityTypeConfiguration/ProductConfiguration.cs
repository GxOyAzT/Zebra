using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zebra.ProductService.Domain.Entities;

namespace Zebra.ProductService.Persistance.Context.EntityTypeConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductModel>
    {
        public void Configure(EntityTypeBuilder<ProductModel> builder)
        {
        }
    }
}
