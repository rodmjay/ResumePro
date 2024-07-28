using ResumePro.Shared.Models;

namespace ResumePro.Shared.Events;

public class ReferenceCreatedEvent(ReferenceDto reference) : BaseEvent(EventType.Created)
{
    public ReferenceDto Reference { get; } = reference;
    protected override string Name => "Reference";
}