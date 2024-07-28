using ResumePro.Shared.Models;

namespace ResumePro.Shared.Events;

public class PersonCreatedEvent(PersonaDetails person) : BaseEvent(EventType.Created)
{
    protected override string Name { get; } = $"Person: '{person.LastName}, {person.FirstName}'";
}