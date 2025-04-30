using Basket.Api.Models;
using FluentValidation;

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
public class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        ShoppingCart cart = request.Cart;
        return await Task.FromResult(new StoreBasketResult(cart.UserName));
    }
}