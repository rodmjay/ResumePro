using ResumePro.Shared.Models;

namespace ResumePro.Shared.Events;

public class SchoolCreatedEvent(SchoolDetails school) : BaseEvent(EventType.Created)
{
    public SchoolDetails School { get; } = school;
    protected override string Name { get; } = $"School: '{school.Name}'";
}