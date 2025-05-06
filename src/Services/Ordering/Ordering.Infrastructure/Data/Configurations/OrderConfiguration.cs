using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(orderId => orderId.Value, dbId => OrderId.Of(dbId));

        builder.HasOne<Customer>().WithMany().HasForeignKey(x => x.CustomerId).IsRequired();

        builder.HasMany<OrderItem>().WithOne().HasForeignKey(x => x.OrderId);

        builder.ComplexProperty(
            x => x.OrderName, builder => 
            { 
                builder.Property(x => x.Value).HasColumnName(nameof(Order.OrderName)).HasMaxLength(100).IsRequired(); 
            });

        builder.ComplexProperty(
            x => x.ShippingAddress, builder =>
            {
                builder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
                builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
                builder.Property(x => x.Email).HasMaxLength(50);
                builder.Property(x => x.AddressLine).HasMaxLength(180).IsRequired();
                builder.Property(x => x.City).HasMaxLength(50);
                builder.Property(x => x.State).HasMaxLength(50);
                builder.Property(x => x.ZipCode).HasMaxLength(5).IsRequired();
                builder.Property(x => x.Country).HasMaxLength(50);
            });

        builder.ComplexProperty(
            x => x.BillingAddress, builder =>
            {
                builder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
                builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
                builder.Property(x => x.Email).HasMaxLength(50);
                builder.Property(x => x.AddressLine).HasMaxLength(180).IsRequired();
                builder.Property(x => x.City).HasMaxLength(50);
                builder.Property(x => x.State).HasMaxLength(50);
                builder.Property(x => x.ZipCode).HasMaxLength(5).IsRequired();
                builder.Property(x => x.Country).HasMaxLength(50);
            });

        builder.ComplexProperty(
            x => x.Payment, builder => 
            {
                builder.Property(x => x.CardName).HasMaxLength(50);
                builder.Property(x => x.CardNumber).HasMaxLength(24).IsRequired();
                builder.Property(x => x.Expiration).HasMaxLength(10);
                builder.Property(x => x.CVV).HasMaxLength(3);
                builder.Property(x => x.PaymentMethod);
            });

        builder.Property(x => x.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(status => status.ToString(), dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

        builder.Property(x => x.TotalPrice);
    }
}
