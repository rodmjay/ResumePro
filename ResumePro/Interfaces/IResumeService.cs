﻿#region Header Info

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
    Task<T> GetResume<T>(int organizationId, int resumeId) where T : ResumeDto;

    Task<OneOf<ResumeDetails, Result>> CreateResume(int organizationId, int personaId, CreateResumeOptions options);
}