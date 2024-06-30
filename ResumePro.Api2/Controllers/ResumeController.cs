using Microsoft.AspNetCore.Mvc;
using ResumePro.Core.Middleware.Bases;
using ResumePro.Services;
using ResumePro.Shared;

namespace ResumePro.Api2.Controllers
{
    public class ResumeController : BaseController
    {
        private readonly IResumeService _resumeService;

        public ResumeController(IServiceProvider serviceProvider, IResumeService resumeService) : base(serviceProvider)
        {
            _resumeService = resumeService;
        }

        [HttpGet("{resumeId}")]
        public Task<ResumeDetails> Get([FromRoute] int resumeId)
        {
            return _resumeService.GetResume<ResumeDetails>(resumeId);
        }
    }
}
