namespace ResumePro.Shared.Events;

public class PersonDeletedEvent() : BaseEvent(EventType.Deleted)
{
    protected override string Name => $"Person";
}