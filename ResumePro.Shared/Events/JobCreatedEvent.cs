using ResumePro.Shared.Models;

namespace ResumePro.Shared.Events;

public class JobCreatedEvent(JobDetails job) : BaseEvent(EventType.Created)
{
    public JobDetails Job { get; } = job;
    protected override string Name => $"Job: '{Job.JobTitle}";
}