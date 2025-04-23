using MediatR;

namespace Ordering.Domain.Abstractions.Interfaces;

public interface IDomainEvent : INotification
{
    Guid EventId => Guid.NewGuid();
    public DateTime OccurredOn => DateTime.Now;
    public string? EventType => GetType().AssemblyQualifiedName;
}
