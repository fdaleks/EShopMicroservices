namespace Shopping.Web.Models.Basket;

public class ShoppingCardModel
{
    public string UserName { get; set; } = default!;
    public List<ShoppingCardItemModel> Items { get; set; } = [];
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
}

public class ShoppingCardItemModel
{
    public Guid ProductId { get; set; }
    public required string ProductName { get; set; }
    public required string Color { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

// wrapper classes
public record GetBasketResponse(ShoppingCardModel Card);
public record StoreBasketRequest(ShoppingCardModel Card);
public record StoreBasketResponse(string UserName);
public record DeleteBasketResponse(bool IsSuccess);
