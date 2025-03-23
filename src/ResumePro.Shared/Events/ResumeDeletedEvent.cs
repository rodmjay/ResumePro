namespace ResumePro.Shared.Events;

public class ResumeDeletedEvent : BaseEvent
{
    public ResumeDeletedEvent() : base(EventType.Deleted)
    {
    }

    protected override string Name => "Resume";
}