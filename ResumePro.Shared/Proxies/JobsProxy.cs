#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public class JobsProxy : BaseProxy, IJobsController
{
    public JobsProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<List<JobDetails>> GetJobs(int personId)
    {
        throw new NotImplementedException();
    }

    public async Task<JobDetails> GetJob(int personId, int jobId)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<JobDetails>> CreateJob(int personId, JobOptions options)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<JobDetails>> UpdateJob(int personId, int jobId, JobOptions options)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> DeleteJob(int personId, int jobId)
    {
        throw new NotImplementedException();
    }
}