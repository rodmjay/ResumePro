namespace ResumePro.Shared.Events;

public class ResumeUpdatedEvent : BaseEvent
{
    public ResumeUpdatedEvent(ResumeDetails resume) : base(EventType.Updated)
    {
        Resume = resume;
        Name = $"Resume: {resume.JobTitle}";
    }

    public ResumeDetails Resume { get; }
    protected override string Name { get; }
}