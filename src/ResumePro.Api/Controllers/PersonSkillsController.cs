using ResumePro.Api.Interfaces;
using ResumePro.Services.Interfaces;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/skills")]
public sealed class PersonSkillsController : BaseController, IPersonSkillsController
{
    private readonly IPersonaSkillService _skillService;

    public PersonSkillsController(IServiceProvider serviceProvider, IPersonaSkillService skillService) : base(
        serviceProvider)
    {
        _skillService = skillService;
    }

    [HttpGet]
    public async Task<List<PersonaSkillDto>> GetSkills([FromRoute] int personId)
    {
        return await _skillService.GetPersonaSkills<PersonaSkillDto>(OrganizationId, personId)
            .ConfigureAwait(false);
    }

    [HttpPatch("{skillId}")]
    public async Task<Result> ToggleSkill([FromRoute] int personId, [FromRoute] int skillId)
    {
        return await _skillService.TogglePersonalSkill(OrganizationId, personId, skillId)
            .ConfigureAwait(false);
    }
}