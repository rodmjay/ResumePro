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

public interface IJobService : IService<Job>
{
    Task<T> GetJob<T>(int organizationId, int personaId, int jobId) where T : JobDto;
    Task<OneOf<JobDetails, Result>> CreateJob(int organizationId, int personId, CreateJobOptions options);
}