#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Proxies;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/skills")]
public class PersonSkillsController : BaseController, IPersonSkillsController
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

    [HttpPatch]
    public async Task<Result> AddOrUpdateSkill([FromRoute] int personId, [FromBody] PersonaSkillsOptions options)
    {
        return await _skillService.AddOrUpdatePersonaSkill(OrganizationId, personId, options)
            .ConfigureAwait(false);
    }

    [HttpDelete("{skillId}")]
    public async Task<Result> DeletePersonalSkill([FromRoute] int personId, [FromRoute] int skillId)
    {
        return await _skillService.DeletePersonalSkill(OrganizationId, personId, skillId)
            .ConfigureAwait(false);
    }
}