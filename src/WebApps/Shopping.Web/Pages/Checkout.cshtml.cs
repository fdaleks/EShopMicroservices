namespace Shopping.Web.Pages
{
    public class CheckoutModel(IBasketService basketService, ILogger<CheckoutModel> logger) : PageModel
    {
        [BindProperty]
        public BasketCheckoutModel Order { get; set; } = default!;

        public ShoppingCardModel Card { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            Card = await basketService.LoadUserBasket("user2");
            
            return Page();
        }

        public async Task<IActionResult> OnPostCheckOutAsync()
        {
            logger.LogInformation("'Checkout' button clicked");

            Card = await basketService.LoadUserBasket("user2");
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            Order.OrderName = "5KHYQ";

            Order.CustomerId = new Guid("9f507f8f-305d-4032-81a6-32cabc6688a4");
            Order.UserName = Card.UserName;
            Order.TotalPrice = Card.TotalPrice;

            await basketService.BasketCheckout(new BasketCheckoutRequest(Order));

            return RedirectToPage("Confirmation", "OrderSubmitted");
        }
    }
}
