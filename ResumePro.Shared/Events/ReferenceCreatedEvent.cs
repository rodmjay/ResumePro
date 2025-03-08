namespace ResumePro.Shared.Events;

public class ReferenceCreatedEvent : BaseEvent
{
    public ReferenceCreatedEvent(ReferenceDto reference) : base(EventType.Created)
    {
        Reference = reference;
    }

    public ReferenceDto Reference { get; }
    protected override string Name => "Reference";
}