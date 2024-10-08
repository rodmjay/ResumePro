﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Services.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Interfaces;

public interface IJobService : IService<Job>
{
    Task<List<T>> GetJobs<T>(int organizationId, int personId) where T : JobDto;
    Task<T> GetJob<T>(int organizationId, int personId, int jobId) where T : JobDto;
    Task<OneOf<JobDetails, Result>> CreateJob(int organizationId, int personId, JobOptions options);
    Task<OneOf<JobDetails, Result>> UpdateJob(int organizationId, int personId, int jobId, JobOptions options);
    Task<Result> DeleteJob(int organizationId, int personId, int jobId);
}