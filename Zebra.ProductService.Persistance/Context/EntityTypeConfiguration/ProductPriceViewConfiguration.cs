using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zebra.ProductService.Domain.Views;

namespace Zebra.ProductService.Persistance.Context.EntityTypeConfiguration
{
    public class ProductPriceViewConfiguration : IEntityTypeConfiguration<ProductPriceModel>
    {
        public void Configure(EntityTypeBuilder<ProductPriceModel> builder)
        {
            builder
                .HasNoKey()
                .ToView("ProductPriceListring");
        }
    }
}
