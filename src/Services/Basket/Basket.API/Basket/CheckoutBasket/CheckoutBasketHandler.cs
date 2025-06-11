using Basket.API.DTOs;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;
public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto can't be null");
        RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public class CheckoutBasketHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        // get existing basket with total price
        var basket = await repository.GetBasket(command.BasketCheckoutDto.UserName, cancellationToken);
        if (basket is null) 
        {
            return new CheckoutBasketResult(false);
        }

        // set totalprice on the 'basket checkout event' message
        var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;
        eventMessage.OrderItems = basket.Items.Select(x => new OrderItem(x.ProductId, x.Quantity, x.Price)).ToList();

        // send basket checkout event to the rabbitmq using masstransit
        await publishEndpoint.Publish(eventMessage, cancellationToken);

        // delete the basket, return the result
        var result = await repository.DeleteBasket(command.BasketCheckoutDto.UserName, cancellationToken);
        return new CheckoutBasketResult(result);
    }
}
