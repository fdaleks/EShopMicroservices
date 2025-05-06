using Ordering.Domain.Abstractions.Interfaces;

namespace Ordering.Domain.Events;

public record OrderUpdatedEvent(Order Order) : IDomainEvent;
