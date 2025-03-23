namespace ResumePro.Shared.Events;

public class SchoolCreatedEvent : BaseEvent
{
    public SchoolCreatedEvent(SchoolDetails school) : base(EventType.Created)
    {
        School = school;
        Name = $"School: '{school.Name}'";
    }

    public SchoolDetails School { get; }
    protected override string Name { get; }
}