using ResumePro.Shared.Models;

namespace ResumePro.Shared.Events;

public class CertificationCreatedEvent(CertificationDto certification) : BaseEvent(EventType.Created)
{
    protected override string Name => "Certification";
}