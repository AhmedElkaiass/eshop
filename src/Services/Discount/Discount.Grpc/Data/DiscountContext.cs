using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public class DiscountContext(DbContextOptions<DiscountContext> options)
    : DbContext(options)
{
    public DbSet<CoponModel> CoponModels { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CoponModel>()
            .HasData([
                new CoponModel
                {
                    Id = 1,
                    ProductName = "Iphone x",
                    Discription = "iphone x discount",
                    Amount = 100
                },
                new CoponModel
                {
                    Id = 2,
                    ProductName = "Samsung s10",
                    Discription = "Samsung s10 discount",
                    Amount = 150
                },
                ]);
    }
}
