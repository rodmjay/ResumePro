using ResumePro.Shared.Models;

namespace ResumePro.Shared.Events;

public class JobUpdatedEvent(CompanyDetails company) : BaseEvent(EventType.Updated)
{
    public CompanyDetails Company { get; } = company;
    protected override string Name => $"Job: {Company.JobTitle}";
}