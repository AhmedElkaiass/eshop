using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Enums;

namespace Order.Infrastrucre.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Domain.Entities.Order>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Order> builder)
    {
        builder.ToTable("Orders");
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id)
            .HasConversion(orderId => orderId.Value,
                                                  value => OrderId.Of(value));


        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(builder => builder.CustomerId)
            .IsRequired();

        builder.HasMany(x => x.OrderItems)
            .WithOne()
            .HasForeignKey(x => x.OrderId);
        builder.ComplexProperty
            (o => o.OrderName, nameBuilder =>
        {
            nameBuilder.Property(n => n.Value)
            .HasColumnName(nameof(Domain.Entities.Order.OrderName))
            .HasMaxLength(100)
            .IsRequired();
        });
        builder.ComplexProperty(x => x.BillingAddress, billingBuilder =>
        {
            billingBuilder.Property(b => b.FirstName)
                .HasMaxLength(100)
                .IsRequired();
            billingBuilder.Property(b => b.LastName)
                .HasMaxLength(100)
                .IsRequired();
            billingBuilder.Property(b => b.Email)
                .HasMaxLength(50);
            billingBuilder.Property(b => b.ZipCode)
                .HasMaxLength(20)
                .IsRequired();
        });
        builder.ComplexProperty(x => x.ShippingAddress, billingBuilder =>
        {
            billingBuilder.Property(b => b.FirstName)
                .HasMaxLength(100)
                .IsRequired();
            billingBuilder.Property(b => b.LastName)
                .HasMaxLength(100)
                .IsRequired();
            billingBuilder.Property(b => b.Email)
                .HasMaxLength(50);
            billingBuilder.Property(b => b.ZipCode)
                .HasMaxLength(20)
                .IsRequired();
        });
        builder.ComplexProperty(x => x.Payment, paymentBuilder =>
        {
            paymentBuilder.Property(b => b.CardNumber)
            .HasColumnName(nameof(Domain.Entities.Order.Payment.CardNumber))
                .HasMaxLength(16)
                .IsRequired();
            paymentBuilder.Property(b => b.CardName)
            .HasColumnName(nameof(Domain.Entities.Order.Payment.CardName))
                .HasMaxLength(100)
                .IsRequired();

            paymentBuilder.Property(b => b.Cvv)
            .HasColumnName(nameof(Domain.Entities.Order.Payment.Cvv))
                .HasMaxLength(3)
                .IsRequired();
            paymentBuilder.Property(b => b.Expiration)
            .HasColumnName(nameof(Domain.Entities.Order.Payment.Expiration))
                .IsRequired();
            paymentBuilder.Property(b => b.PaymentMethodId)
               .IsRequired();

        });
        builder.Property(o => o.OrderDate).IsRequired();
        builder.Property(o => o.Status).IsRequired();
        builder.Property(o => o.Status).HasConversion(
            status => status.ToString(),
            status => (OrderStatus)Enum.Parse(typeof(OrderStatus), status));
        builder.Property(o => o.TotalPrice).IsRequired().HasColumnType("decimal(18,2)");
    }
}
