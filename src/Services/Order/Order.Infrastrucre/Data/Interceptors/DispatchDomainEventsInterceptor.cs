using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Order.Infrastrucre.Data.Interceptors;

public class DispatchDomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchEvnets(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);

    }
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await DispatchEvnets(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    private async Task DispatchEvnets(DbContext? context)
    {
        if (context == null) return;
        var aggregates = context.ChangeTracker
            .Entries<IAggregateRoot>()
            .Where(x => x.Entity.DomainEvents.Any())
            .Select(x => x.Entity)
            .ToList();

        var domainEvents = aggregates.SelectMany(e => e.DomainEvents);

        aggregates.ToList().ForEach(aggregates => aggregates.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}
