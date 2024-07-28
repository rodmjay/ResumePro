namespace ResumePro.Shared.Events;

public class JobDeletedEvent() : BaseEvent(EventType.Deleted)
{
    protected override string Name => "Job";
}