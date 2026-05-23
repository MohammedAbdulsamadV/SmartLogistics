namespace Logistics.Domain.Common;

public abstract record DomainEvent():IDomainEvent
{
    public DateTime OccurredOn { get; protected set; } = DateTime.UtcNow;

    public bool IsPublished { get; set; } = false;}