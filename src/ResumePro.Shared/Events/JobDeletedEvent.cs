namespace ResumePro.Shared.Events;

public class JobDeletedEvent : BaseEvent
{
    public JobDeletedEvent() : base(EventType.Deleted)
    {
    }

    protected override string Name => "Job";
}