namespace Order.Domain.Entities;

public class Product : Entity<ProductId>
{
    public string Name { get; private set; } = default!;
    public decimal Price { get; private set; }
    public static Product Creat(ProductId id, string name, decimal price)
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        return new Product
        {
            Name = name,
            Id = id,
            Price = price
        };
    }

}