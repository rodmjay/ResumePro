#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Services.Interfaces;
using ResumePro.Entities;
using ResumePro.Shared;

namespace ResumePro.Interfaces;

public interface IResumeService : IService<Resume>
{
    Task<T> GetResume<T>(int organizationId, int resumeId) where T : ResumeDto;
}