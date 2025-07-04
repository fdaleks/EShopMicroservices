namespace Shopping.Web.Pages;

public class IndexModel(ICatalogService catalogService, IBasketService basketService, ILogger<IndexModel> logger) : PageModel
{
    public IEnumerable<ProductModel> ProductList { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        logger.LogInformation("Index page visited");
        var result = await catalogService.GetProducts();
        ProductList = result.Products;
        return Page();
    }

    public async Task<IActionResult> OnPostAddToCardAsync(Guid productId)
    {
        logger.LogInformation("'Add to card' button clicked");
        var productResponse = await catalogService.GetProductById(productId);
        var basket = await basketService.LoadUserBasket("user2");

        basket.Items.Add(new ShoppingCardItemModel 
        {
            ProductId = productId,
            ProductName = productResponse.Product.Name,
            Color = "black",
            Quantity = 1,
            Price = productResponse.Product.Price
        });

        await basketService.StoreBasket(new StoreBasketRequest(basket));
        return RedirectToPage("Card");
    }
}
