#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Services.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Interfaces;

public interface IResumeService : IService<Resume>
{
    Task<T> GetResume<T>(int organizationId, int personId, int resumeId) where T : ResumeDto;
    Task<List<T>> GetResumes<T>(int organizationId, int personaId) where T : ResumeDto;
    Task<OneOf<ResumeDetails, Result>> CreateResume(int organizationId, int personaId, ResumeOptions options);

    Task<OneOf<ResumeDetails, Result>> UpdateResume(int organizationId, int personaId, int resumeId,
        ResumeOptions options);

    Task<string> SaveResumeAsPdf(int organizationId, int personId, int resumeId);

    Task<Result> DeleteResume(int organizationId, int personaId, int resumeId);
    Task<OneOf<string, Result>> Generate(ResumeDetails resumeDetails, int templateId);
    Task<ResumeDetails> Generate(int organizationId, int personId, int resumeId);
}