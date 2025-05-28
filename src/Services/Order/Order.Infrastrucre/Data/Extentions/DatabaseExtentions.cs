using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Order.Infrastrucre.Data.Extentions;

public static class DatabaseExtentions
{
    public static async Task InitDatabaseAsync(this WebApplication app)
    {
        // Initialize the database here, e.g., apply migrations, seed data, etc.
        using (var scope = app.Services.CreateScope())
        {
            ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.MigrateAsync().GetAwaiter().GetResult();
            await SeedAsync(dbContext);
        }
    }

    private static async Task SeedAsync(ApplicationDbContext dbContext)
    {
        await SeedCustomers(dbContext);
        await SeedProductsAsync(dbContext);
        await SeedOrdersAsync(dbContext);
    }

    private static async Task SeedOrdersAsync(ApplicationDbContext dbContext)
    {
        if (!await dbContext.Orders.AnyAsync())
        {
            await dbContext.Orders.AddRangeAsync(InitalData.Orders);
            await dbContext.SaveChangesAsync();
        }
    }

    private static async Task SeedProductsAsync(ApplicationDbContext dbContext)
    {
        if (!await dbContext.Products.AnyAsync())
        {
            await dbContext.Products.AddRangeAsync(InitalData.Products);
            await dbContext.SaveChangesAsync();
        }
    }

    private static async Task SeedCustomers(ApplicationDbContext dbContext)
    {
        if (!await dbContext.Customers.AnyAsync())
        {
            await dbContext.Customers.AddRangeAsync(InitalData.Customers);
            await dbContext.SaveChangesAsync();
        }
    }
}
