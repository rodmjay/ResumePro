namespace ResumePro.Shared.Events;

public class ReferenceUpdatedEvent : BaseEvent
{
    public ReferenceUpdatedEvent(ReferenceDto reference) : base(EventType.Updated)
    {
        Reference = reference;
    }

    public ReferenceDto Reference { get; }
    protected override string Name => "Reference";
}