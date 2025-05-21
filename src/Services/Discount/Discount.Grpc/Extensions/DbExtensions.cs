namespace Discount.Grpc.Extensions;

public static class DbExtensions
{
    public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountDbContext>();
        dbContext.Database.MigrateAsync();

        return app;
    }
}
