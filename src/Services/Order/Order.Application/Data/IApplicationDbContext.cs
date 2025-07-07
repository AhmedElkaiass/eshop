using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;

namespace Order.Application.Data;
public interface IApplicationDbContext
{
    DbSet<Order.Domain.Entities.Order> Orders { get; }
    DbSet<Product> Products { get; }
    DbSet<OrderItem> OrderItems { get; }
    DbSet<Customer> Customers { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
