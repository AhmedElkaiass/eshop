using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Order.Infrastrucre.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(p => p.Id);
        builder.Property(c => c.Id)
          .HasConversion(productId => productId.Value, db => ProductId.Of(db));
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
    }
}
