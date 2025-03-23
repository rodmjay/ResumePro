namespace ResumePro.Shared.Events;

public class PersonUpdatedEvent : BaseEvent
{
    public PersonUpdatedEvent(PersonaDetails person) : base(EventType.Updated)
    {
        Name = $"Person: '{person.LastName}, {person.FirstName}'";
    }

    protected override string Name { get; }
}