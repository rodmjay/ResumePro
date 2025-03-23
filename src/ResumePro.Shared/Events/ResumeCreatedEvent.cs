namespace ResumePro.Shared.Events;

public class ResumeCreatedEvent : BaseEvent
{
    public ResumeCreatedEvent(ResumeDetails resume) : base(EventType.Created)
    {
        Resume = resume;
        Name = $"Resume: {resume.JobTitle}";
    }

    public ResumeDetails Resume { get; }
    protected override string Name { get; }
}