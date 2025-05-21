namespace Discount.Grpc.Data;

public class DiscountDbContext(DbContextOptions<DiscountDbContext> options) : DbContext(options)
{
    public DbSet<Coupon> Coupons { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Coupon>().HasData(
            new Coupon { Id = 1, ProductName = "Pixel Pro 8", Description = "Google Pixel Discount", Amount = 100 },
            new Coupon { Id = 2, ProductName = "Galaxy S24 Ultra", Description = "Samsung discount", Amount = 150 });
    }
}
