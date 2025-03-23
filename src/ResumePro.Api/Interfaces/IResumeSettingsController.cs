namespace ResumePro.Api.Interfaces;

public interface IResumeSettingsController
{
    Task<ActionResult<ResumeSettingsDto>> UpdateSettings(
        int personId, int resumeId,
        ResumeSettingsOptions options);
}