namespace Basket.API.Models;

public class ShoppingCardItem
{
    public Guid ProductId { get; set; }
    public required string ProductName { get; set; }
    public required string Color { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
