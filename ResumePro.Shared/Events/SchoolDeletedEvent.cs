namespace ResumePro.Shared.Events;

public class SchoolDeletedEvent() : BaseEvent(EventType.Deleted)
{
    protected override string Name => "School";
}