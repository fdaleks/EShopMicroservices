namespace Shopping.Web.Pages
{
    public class CardModel(IBasketService basketService, ILogger<CardModel> logger) : PageModel
    {
        public ShoppingCardModel Card {  get; set; } = new ShoppingCardModel();

        public async Task<IActionResult> OnGetAsync()
        {
            Card = await basketService.LoadUserBasket("user2");




            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCardAsync(Guid productId)
        {
            logger.LogInformation("'Remove from card' button clicked");
            Card = await basketService.LoadUserBasket("user2");
            Card.Items.RemoveAll(x => x.ProductId == productId);
            await basketService.StoreBasket(new StoreBasketRequest(Card));
            return RedirectToPage();
        }
    }
}
