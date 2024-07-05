#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using OneOf;
using ResumePro.Core.Services.Interfaces;
using ResumePro.Entities;
using ResumePro.Shared;
using ResumePro.Shared.Common;
using ResumePro.Shared.Options;

namespace ResumePro.Interfaces;

public interface IResumeService : IService<Resume>
{
    Task<T> GetResume<T>(int organizationId, int personId, int resumeId) where T : ResumeDto;
    Task<List<T>> GetResumes<T>(int organizationId, int personaId) where T : ResumeDto;
    Task<OneOf<ResumeDetails, Result>> CreateResume(int organizationId, int personaId, CreateResumeOptions options);
    Task<OneOf<ResumeDetails, Result>> UpdateResume(int organizationId, int personaId, int resumeId, CreateResumeOptions options);
    Task<Result> DeleteResume(int organizationId, int personaId, int resumeId);
}