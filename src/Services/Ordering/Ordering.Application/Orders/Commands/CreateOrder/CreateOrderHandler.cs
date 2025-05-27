namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var newOrder = CreateNewOrder(command.Order);

        dbContext.Orders.Add(newOrder);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateOrderResult(newOrder.Id.Value);
    }

    private static Order CreateNewOrder(OrderDto orderDto)
    {
        var orderId = OrderId.Of(Guid.NewGuid());

        var customerId = CustomerId.Of(orderDto.CustomerId);

        var orderName = OrderName.Of(orderDto.OrderName);

        var shippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName,
            orderDto.ShippingAddress.Email, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.City,
            orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode, orderDto.ShippingAddress.Country);

        var billingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName,
            orderDto.BillingAddress.Email, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.City,
            orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode, orderDto.BillingAddress.Country);

        var payment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber,
            orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod);

        var newOrder = Order.Create(orderId, customerId, orderName, shippingAddress, billingAddress, payment);
        
        foreach(var orderItem in orderDto.OrderItems)
        {
            newOrder.AddOrderItem(ProductId.Of(orderItem.ProductId), orderItem.Quantity, orderItem.Price);
        }

        return newOrder;
    }
}
