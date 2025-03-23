namespace ResumePro.Api.Interfaces;

public interface IResumeController
{
    Task<ResumeDetails> GetResume(int personId, int resumeId);

    Task<List<ResumeDto>> GetResumes(int personId);

    Task<ActionResult<ResumeDetails>> CreateResume(int personId,
        ResumeOptions options);

    Task<ActionResult<ResumeDetails>> UpdateResume(int personId,
        int resumeId,
        ResumeOptions options);

    Task<Result> DeleteResume(int personId,
        int resumeId);
}