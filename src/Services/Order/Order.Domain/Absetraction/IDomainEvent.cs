using MediatR;

namespace Order.Domain.Absetraction;

public interface IDomainEvent : INotification
{
    public Guid EventId => Guid.NewGuid();
    public DateTime OccuredAt => DateTime.Now;
    public string? EventName => GetType().AssemblyQualifiedName;
}
