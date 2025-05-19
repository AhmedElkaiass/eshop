namespace Order.Domain.Events;

public record OrderUpdatedEvent(Entities.Order order) : IDomainEvent;
