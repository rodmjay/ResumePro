namespace ResumePro.Shared.Events;

public class ReferenceDeletedEvent : BaseEvent
{
    public ReferenceDeletedEvent() : base(EventType.Deleted)
    {
    }

    protected override string Name => "Reference";
}