using ResumePro.Shared.Models;

namespace ResumePro.Shared.Events;

public class SchoolUpdatedEvent(SchoolDetails school) : BaseEvent(EventType.Updated)
{
    public SchoolDetails School { get; } = school;
    protected override string Name { get; } = $"School: {school.Name}";
}