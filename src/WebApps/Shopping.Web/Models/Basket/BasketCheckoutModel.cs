﻿namespace Shopping.Web.Models.Basket;

public class BasketCheckoutModel
{
    public string UserName { get; set; } = default!;
    public Guid CustomerId { get; set; } = default;
    public string OrderName { get; set; } = default!;
    public decimal TotalPrice { get; set; }

    // Shipping and Billing Address
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string AddressLine { get; set; } = default!;
    public string City { get; set; } = default!;
    public string State { get; set; } = default!;
    public string ZipCode { get; set; } = default!;
    public string Country { get; set; } = default!;

    // Payment
    public string CardName { get; set; } = default!;
    public string CardNumber { get; set; } = default!;
    public string Expiration { get; set; } = default!;
    public string CVV { get; set; } = default!;
    public int PaymentMethod { get; set; } = default;
}

// wrapper classes
public record BasketCheckoutRequest(BasketCheckoutModel BasketCheckoutDto);
public record BasketCheckoutResponse(bool IsSuccess);
