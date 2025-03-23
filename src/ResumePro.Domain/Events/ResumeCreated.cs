namespace ResumePro.Domain.Events;

public class ResumeCreated
{
    public ResumeCreated(int organizationId, int personId, int resumeId)
    {
        OrganizationId = organizationId;
        PersonId = personId;
        ResumeId = resumeId;
    }

    public int OrganizationId { get; }
    public int PersonId { get; }
    public int ResumeId { get; }
}