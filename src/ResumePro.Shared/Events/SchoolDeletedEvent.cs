namespace ResumePro.Shared.Events;

public class SchoolDeletedEvent : BaseEvent
{
    public SchoolDeletedEvent() : base(EventType.Deleted)
    {
    }

    protected override string Name => "School";
}