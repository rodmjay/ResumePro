namespace ResumePro.Shared.Events;

public class CertificationUpdatedEvent : BaseEvent
{
    public CertificationUpdatedEvent(CertificationDto certification) : base(EventType.Updated)
    {
    }

    protected override string Name => "Certification";
}