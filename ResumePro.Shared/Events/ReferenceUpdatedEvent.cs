using ResumePro.Shared.Models;

namespace ResumePro.Shared.Events;

public class ReferenceUpdatedEvent(ReferenceDto reference) : BaseEvent(EventType.Updated)
{
    public ReferenceDto Reference { get; } = reference;
    protected override string Name => "Reference";
}