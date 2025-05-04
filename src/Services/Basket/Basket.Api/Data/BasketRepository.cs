namespace Basket.Api.Data;

public class BasketRepository(IDocumentSession _session) : IBasketRepository
{

    public async Task<ShoppingCart?> GetBasket(string userName, CancellationToken cancellationToken = default)
    {
        // Check if the userName is null or empty
        if (string.IsNullOrEmpty(userName))
        {
            throw new ArgumentException("User name cannot be null or empty.", nameof(userName));
        }
        // Retrieve the basket from the database using Marten
        return await _session.LoadAsync<ShoppingCart>(userName, cancellationToken);
    }
    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        _session.Store(basket);
        await _session.SaveChangesAsync(cancellationToken);
        return basket;
    }
    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(userName))
            throw new ArgumentException("User name cannot be null or empty.", nameof(userName));
        _session.Delete<ShoppingCart>(userName);
        await _session.SaveChangesAsync(cancellationToken);
        return true;
    }
}
