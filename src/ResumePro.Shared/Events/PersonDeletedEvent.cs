namespace ResumePro.Shared.Events;

public class PersonDeletedEvent : BaseEvent
{
    public PersonDeletedEvent() : base(EventType.Deleted)
    {
    }

    protected override string Name => "Person";
}