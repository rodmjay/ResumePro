namespace ResumePro.Shared.Events;

public class SchoolUpdatedEvent : BaseEvent
{
    public SchoolUpdatedEvent(SchoolDetails school) : base(EventType.Updated)
    {
        School = school;
        Name = $"School: {school.Name}";
    }

    public SchoolDetails School { get; }
    protected override string Name { get; }
}