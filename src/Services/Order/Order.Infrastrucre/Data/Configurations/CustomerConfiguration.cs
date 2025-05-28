using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Order.Infrastrucre.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasConversion(customerId => customerId.Value, db => CustomerId.Of(db));
        builder.Property(c => c.FullName).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Email).IsRequired().HasMaxLength(100);
        builder.HasIndex(c => c.Email).IsUnique();
        builder.Property(c => c.PhoneNumber).IsRequired(false).HasMaxLength(15);
    }
}
