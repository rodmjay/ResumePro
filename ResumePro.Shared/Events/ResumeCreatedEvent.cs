using ResumePro.Shared.Models;

namespace ResumePro.Shared.Events
{
    public class ResumeCreatedEvent(ResumeDetails resume) : BaseEvent(EventType.Created)
    {
        public ResumeDetails Resume { get; } = resume;
        protected override string Name { get; } = $"Resume: {resume.JobTitle}";
    }
}
