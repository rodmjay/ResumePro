using ResumePro.Core.Middleware.Bases;

namespace ResumePro.Api.Controllers;

[Route("v1.0/companies/{companyId}/job-postings")]
public class JobPostingsController : BaseController
{
    public JobPostingsController(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}