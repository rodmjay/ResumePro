namespace ResumePro.Shared.Events;

public class JobUpdatedEvent : BaseEvent
{
    public JobUpdatedEvent(CompanyDetails company) : base(EventType.Updated)
    {
        Company = company;
    }

    public CompanyDetails Company { get; }
    protected override string Name => $"Job: {Company.JobTitle}";
}