﻿using ResumePro.Core.Services.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Interfaces;

public interface IResumeSettingsService : IService<ResumeSettings>
{
    Task<ResumeSettingsDto> GetResumeSettings(int organizationId, int personId, int resumeId);

    Task<OneOf<ResumeSettingsDto, Result>> AddOrUpdateUpdateResumeSettings(int organizationId, int personId, int resumeId,
        ResumeSettingsOptions options);
}