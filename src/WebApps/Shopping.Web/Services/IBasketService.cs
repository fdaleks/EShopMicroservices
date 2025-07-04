namespace Shopping.Web.Services;

public interface IBasketService
{
    [Get("/basket-service/basket/{userName}")]
    Task<GetBasketResponse> GetBasket(string userName);

    [Post("/basket-service/basket")]
    Task<StoreBasketResponse> StoreBasket(StoreBasketRequest request);

    [Delete("/basket-service/basket/{userName}")]
    Task<DeleteBasketResponse> DeleteBasket(string userName);

    [Post("/basket-service/basket/checkout")]
    Task<BasketCheckoutResponse> BasketCheckout(BasketCheckoutRequest request);

    public async Task<ShoppingCardModel> LoadUserBasket(string userName)
    {
        ShoppingCardModel basket;

        try
        {
            var getBasketResponse = await GetBasket(userName);
            basket = getBasketResponse.Card;
        }
        catch (ApiException apiException) when (apiException.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            basket = new ShoppingCardModel
            {
                UserName = userName,
                Items = []
            };
        }

        return basket;
    }
}
