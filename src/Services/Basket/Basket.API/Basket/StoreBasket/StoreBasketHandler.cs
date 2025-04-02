using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCard Card) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Card).NotNull().WithMessage("Card can't be null");
        RuleFor(x => x.Card.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

internal class StoreBasketCommandHandler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProto) 
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await CalculateDiscount(command.Card, cancellationToken);

        ShoppingCard basket = await repository.StoreBasket(command.Card, cancellationToken);

        return new StoreBasketResult(basket.UserName);
    }

    private async Task CalculateDiscount(ShoppingCard shoppingCard, CancellationToken cancellationToken)
    {
        foreach (var item in shoppingCard.Items)
        {
            var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
            item.Price -= coupon.Amount;
        }
    }
}
