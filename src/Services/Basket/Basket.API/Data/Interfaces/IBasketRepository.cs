namespace Basket.API.Data.Interfaces;

public interface IBasketRepository
{
    Task<ShoppingCard> StoreBasket(ShoppingCard basket, CancellationToken cancellationToken = default);
    Task<ShoppingCard> GetBasket(string userName, CancellationToken cancellationToken = default);
    Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default);
}
