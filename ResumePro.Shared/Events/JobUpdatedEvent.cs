using ResumePro.Shared.Models;

namespace ResumePro.Shared.Events;

public class JobUpdatedEvent(JobDetails job) : BaseEvent(EventType.Updated)
{
    public JobDetails Job { get; } = job;
    protected override string Name => $"Job: {Job.JobTitle}";
}