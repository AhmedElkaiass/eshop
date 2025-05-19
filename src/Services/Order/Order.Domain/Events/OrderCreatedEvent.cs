namespace Order.Domain.Events;

public record OrderCreatedEvent(Entities.Order order) : IDomainEvent;
