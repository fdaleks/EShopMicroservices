using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication application)
    {
        using var scope = application.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<DataContext>();

        context.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedAsync(context);
    }

    private static async Task SeedAsync(DataContext context)
    {
        await SeedCustomersAsync(context);
        await SeedProductsAsync(context);
        await SeedOrdersAndItemsAsync(context);
    }

    private static async Task SeedCustomersAsync(DataContext context)
    {
        if (!await context.Customers.AnyAsync())
        {
            await context.Customers.AddRangeAsync(OrderingInitialData.Customers);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedProductsAsync(DataContext context)
    {
        if (!await context.Products.AnyAsync())
        {
            await context.Products.AddRangeAsync(OrderingInitialData.Products);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedOrdersAndItemsAsync(DataContext context)
    {
        if (!await context.Orders.AnyAsync())
        {
            await context.Orders.AddRangeAsync(OrderingInitialData.OrdersWithItems);
            await context.SaveChangesAsync();
        }
    }
}
