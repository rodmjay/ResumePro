using ResumePro.Core.Middleware.Bases;

namespace ResumePro.Api.Controllers;

[Route("v1.0/companies/{companyId}/hiring-managers")]
public class HiringManagersController : BaseController
{
    public HiringManagersController(IServiceProvider serviceProvider) : base(serviceProvider)
    { 
    }
}