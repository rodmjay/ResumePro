namespace ResumePro.Shared.Events;

public class CertificationCreatedEvent : BaseEvent
{
    public CertificationCreatedEvent(CertificationDto certification) : base(EventType.Created)
    {
    }

    protected override string Name => "Certification";
}