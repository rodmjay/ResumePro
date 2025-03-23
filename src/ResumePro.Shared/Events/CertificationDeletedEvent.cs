namespace ResumePro.Shared.Events;

public class CertificationDeletedEvent : BaseEvent
{
    public CertificationDeletedEvent() : base(EventType.Deleted)
    {
    }

    protected override string Name => "Certification";
}