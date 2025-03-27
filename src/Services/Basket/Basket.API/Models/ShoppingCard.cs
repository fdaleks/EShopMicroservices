namespace Basket.API.Models;

public class ShoppingCard
{
    public required string UserName { get; set; }
    public List<ShoppingCardItem> Items { get; set; } = [];
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

    public ShoppingCard(string userName)
    {
        UserName = userName;
    }

    // Required for mapping
    public ShoppingCard()
    {
    }
}
