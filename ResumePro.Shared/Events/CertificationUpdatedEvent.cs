using ResumePro.Shared.Models;

namespace ResumePro.Shared.Events;

public class CertificationUpdatedEvent(CertificationDto certification) : BaseEvent(EventType.Updated)
{
    protected override string Name => "Certification";
}