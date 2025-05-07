namespace Basket.Api.Basket.StoreBasket;
public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);
public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart Cannt be null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("user is required ");
        RuleFor(x => x.Cart.Items).NotEmpty()
            .Must(x => x.Count > 0)
            .WithMessage("should have one item atleast.");
    }
}
public class StoreBasketCommandHandler(IBasketRepository basketRepository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        var shoppingCart = await basketRepository.StoreBasket(command.Cart, cancellationToken);
        return await Task.FromResult(new StoreBasketResult(shoppingCart.UserName));
    }
}