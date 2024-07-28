namespace ResumePro.Shared.Events;

public class ReferenceDeletedEvent() : BaseEvent(EventType.Deleted)
{
    protected override string Name => "Reference";
}