using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Interfaces;

public interface IIndividualJobsController
{
    Task<List<JobDetails>> GetJobs();
    Task<JobDetails> GetJob( int jobId);
    Task<ActionResult<JobDetails>> CreateJob( JobOptions options);

    Task<ActionResult<JobDetails>> UpdateJob( int jobId,
        JobOptions options);

    Task<Result> DeleteJob( int jobId);
}