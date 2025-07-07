using Order.Application.Data;

namespace Order.Application.Orders.Commands.CreateOrder;

internal sealed class CreateOrderCommandHandler(IApplicationDbContext _context) : ICommandHandler<CreateOrderCommand, CreateOrederCommandResult>
{
    public async Task<CreateOrederCommandResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {

        throw new NotImplementedException();
    }
}