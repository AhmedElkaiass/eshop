
using FluentValidation;

namespace Order.Application.Orders.Commands.CreateOrder;
public record CreateOrderCommand(OrderDto order) : ICommand<CreateOrederCommandResult>;
public record CreateOrederCommandResult(Guid Id);
public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator( )
    {
        RuleFor(x => x.order).NotNull().WithMessage("Order cannot be null");
        RuleFor(x => x.order.OrderName).NotEmpty().WithMessage("Order name is required");
        RuleFor(x => x.order.CustomerId).NotEmpty().WithMessage("Customer ID is required");
        RuleFor(x => x.order.OrderItems).NotEmpty().WithMessage("Order must have at least one item");
    }
}