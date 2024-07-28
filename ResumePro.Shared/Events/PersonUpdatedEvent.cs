using ResumePro.Shared.Models;

namespace ResumePro.Shared.Events;

public class PersonUpdatedEvent(PersonaDetails person) : BaseEvent(EventType.Updated)
{
    protected override string Name { get; } = $"Person: '{person.LastName}, {person.FirstName}'";

}