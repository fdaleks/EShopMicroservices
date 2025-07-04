using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shopping.Web.Pages
{
    public class ProductDetailsModel(ICatalogService catalogService, IBasketService basketService, ILogger<ProductDetailsModel> logger) : PageModel
    {
        public ProductModel Product { get; set; } = default!;

        [BindProperty]
        public string Color { get; set; } = default!;

        [BindProperty]
        public int Quantity { get; set; } = default;

        public async Task<IActionResult> OnGetAsync(Guid productId)
        {
            var response = await catalogService.GetProductById(productId);
            Product = response.Product;

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
}
