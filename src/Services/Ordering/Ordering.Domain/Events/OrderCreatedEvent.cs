using Ordering.Domain.Abstractions.Interfaces;

namespace Ordering.Domain.Events;

public record OrderCreatedEvent(Order Order) : IDomainEvent;
