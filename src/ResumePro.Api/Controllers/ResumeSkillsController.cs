using ResumePro.Api.Interfaces;
using ResumePro.Services.Interfaces;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/resume/{resumeId}/skills")]
public sealed class ResumeSkillsController : BaseController, IResumeSkillsController
{
    private readonly IResumeSkillService _resumeSkillService;

    public ResumeSkillsController(IServiceProvider serviceProvider, IResumeSkillService resumeSkillService) : base(
        serviceProvider)
    {
        _resumeSkillService = resumeSkillService;
    }

    [HttpPatch("{skillId}")]
    public async Task<Result> AddResumeSkill([FromRoute] int personId, [FromRoute] int resumeId,
        [FromRoute] int skillId)
    {
        return await _resumeSkillService.AddResumeSkill(OrganizationId, personId, resumeId, skillId)
            .ConfigureAwait(false);
    }

    [HttpDelete("{skillId}")]
    public async Task<Result> DeleteResumeSkill([FromRoute] int personId, [FromRoute] int resumeId,
        [FromRoute] int skillId)
    {
        return await _resumeSkillService.DeleteResumeSkill(OrganizationId, personId, resumeId, skillId)
            .ConfigureAwait(false);
    }
}