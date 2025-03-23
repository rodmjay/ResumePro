using Bespoke.Services.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Services.Interfaces;

public interface IResumeSettingsService : IService<ResumeSettings>
{
    Task<ResumeSettingsDto> GetResumeSettings(int organizationId, int personId, int resumeId);

    Task<OneOf<ResumeSettingsDto, Result>> AddOrUpdateUpdateResumeSettings(int organizationId, int personId,
        int resumeId,
        ResumeSettingsOptions options);
}