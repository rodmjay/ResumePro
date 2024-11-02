using ResumePro.Shared.Models;

namespace ResumePro.Shared.Events;

public class JobCreatedEvent(CompanyDetails company) : BaseEvent(EventType.Created)
{
    public CompanyDetails Company { get; } = company;
    protected override string Name => $"Job: '{Company.JobTitle}";
}