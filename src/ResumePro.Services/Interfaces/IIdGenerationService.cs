namespace ResumePro.Services.Interfaces;

public interface IIdGenerationService
{
    Task<int> GetNextResumeId(int organizationId);
}