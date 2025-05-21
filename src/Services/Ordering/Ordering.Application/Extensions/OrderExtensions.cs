namespace Ordering.Application.Extensions;

public static class OrderExtensions
{
    public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
    {
        var result = orders.Select(order => new OrderDto(
            Id: order.Id.Value,
            CustomerId: order.CustomerId.Value,
            OrderName: order.OrderName.Value,
            ShippingAddress: new AddressDto(order.ShippingAddress.FirstName, order.ShippingAddress.LastName,
                order.ShippingAddress.Email, order.ShippingAddress.AddressLine, order.ShippingAddress.City,
                order.ShippingAddress.State, order.ShippingAddress.ZipCode, order.ShippingAddress.Country),
            BillingAddress: new AddressDto(order.BillingAddress.FirstName, order.BillingAddress.LastName,
                order.BillingAddress.Email, order.BillingAddress.AddressLine, order.BillingAddress.City,
                order.BillingAddress.State, order.BillingAddress.ZipCode, order.BillingAddress.Country),
            Payment: new PaymentDto(order.Payment.CardName, order.Payment.CardName,
                order.Payment.Expiration, order.Payment.CVV, order.Payment.PaymentMethod),
            Status: order.Status,
            OrderItems: order.OrderItems.Select(x => new OrderItemDto(x.OrderId.Value, x.ProductId.Value, x.Quantity, x.Price)).ToList()
        ));

        return result;
    }
}
