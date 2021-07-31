using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zebra.ProductService.Domain.Entities;

namespace Zebra.ProductService.Persistance.Context.EntityTypeConfiguration
{
    class PriceConfiguration : IEntityTypeConfiguration<PriceModel>
    {
        public void Configure(EntityTypeBuilder<PriceModel> builder)
        {
        }
    }
}
