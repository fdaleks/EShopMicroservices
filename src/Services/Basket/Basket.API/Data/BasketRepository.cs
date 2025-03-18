namespace BasketApi.Data;

public class BasketRepository(IDocumentSession session) : IBasketRepository
{
    public async Task<ShoppingCard> StoreBasket(ShoppingCard basket, CancellationToken cancellationToken = default)
    {
        session.Store(basket);
        await session.SaveChangesAsync(cancellationToken);

        return basket;
    }

    public async Task<ShoppingCard> GetBasket(string userName, CancellationToken cancellationToken = default)
    {
        var basket = await session.LoadAsync<ShoppingCard>(userName, cancellationToken) ?? throw new BasketNotFoundException(userName);
        
        return basket;
    }

    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        session.Delete<ShoppingCard>(userName);
        await session.SaveChangesAsync(cancellationToken);

        return true;
    }
}
