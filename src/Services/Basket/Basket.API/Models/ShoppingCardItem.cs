﻿namespace BasketApi.Models;

public class ShoppingCardItem
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = default!;
    public string Color { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
