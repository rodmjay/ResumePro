namespace ResumePro.Shared.Events;

public class PersonCreatedEvent : BaseEvent
{
    public PersonCreatedEvent(PersonaDetails person) : base(EventType.Created)
    {
        Name = $"Person: '{person.LastName}, {person.FirstName}'";
    }

    protected override string Name { get; }
}