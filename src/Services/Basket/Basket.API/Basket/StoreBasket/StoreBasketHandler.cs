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

internal class StoreBasketCommandHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCard basket = await repository.StoreBasket(command.Card, cancellationToken);

        return new StoreBasketResult(basket.UserName);
    }
}
