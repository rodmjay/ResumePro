namespace ResumePro.Shared.Events;

public class CertificationDeletedEvent() : BaseEvent(EventType.Deleted)
{
    protected override string Name => "Certification";

}