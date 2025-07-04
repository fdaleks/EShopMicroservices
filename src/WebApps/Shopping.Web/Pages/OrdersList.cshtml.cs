namespace Shopping.Web.Pages
{
    public class OrdersListModel(IOrderingService orderingService, ILogger<OrdersListModel> logger) : PageModel
    {
        public IEnumerable<OrderModel> Orders { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var customerId = new Guid("9f507f8f-305d-4032-81a6-32cabc6688a4");

            var response = await orderingService.GetOrdersByCustomer(customerId);
            Orders = response.Orders;
            return Page();
        }
    }
}
