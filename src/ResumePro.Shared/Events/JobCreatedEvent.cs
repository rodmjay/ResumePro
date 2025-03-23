namespace ResumePro.Shared.Events;

public class JobCreatedEvent : BaseEvent
{
    public JobCreatedEvent(CompanyDetails company) : base(EventType.Created)
    {
        Company = company;
    }

    public CompanyDetails Company { get; }
    protected override string Name => $"Job: '{Company.JobTitle}";
}