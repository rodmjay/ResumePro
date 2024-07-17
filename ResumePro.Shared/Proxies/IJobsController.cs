#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;


public interface IJobsController
{
    Task<List<JobDetails>> GetJobs(int personId);
    Task<JobDetails> GetJob(int personId, int jobId);
    Task<ActionResult<JobDetails>> CreateJob(int personId, JobOptions options);

    Task<ActionResult<JobDetails>> UpdateJob(int personId,  int jobId,
        JobOptions options);

    Task<Result> DeleteJob(int personId, int jobId);
}