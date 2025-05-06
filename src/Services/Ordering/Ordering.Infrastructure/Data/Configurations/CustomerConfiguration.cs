﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(
            customerId => customerId.Value,     // set value in database via CustomerId.Value
            dbId => CustomerId.Of(dbId));       // get value object from database via CustomerId.Of(id)

        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

        builder.Property(x => x.Email).HasMaxLength(255);
        builder.HasIndex(x => x.Email).IsUnique();
    }
}
