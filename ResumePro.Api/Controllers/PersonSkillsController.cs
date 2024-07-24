#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/skills")]
public sealed class PersonSkillsController(IServiceProvider serviceProvider, IPersonaSkillService skillService)
    : BaseController(serviceProvider), IPersonSkillsController
{
    [HttpGet]
    public async Task<List<PersonaSkillDto>> GetSkills([FromRoute] int personId)
    {
        return await skillService.GetPersonaSkills<PersonaSkillDto>(OrganizationId, personId)
            .ConfigureAwait(false);
    }

    [HttpPatch("{skillId}")]
    public async Task<Result> ToggleSkill([FromRoute] int personId, [FromRoute] int skillId)
    {
        return await skillService.TogglePersonalSkill(OrganizationId, personId, skillId)
            .ConfigureAwait(false);
    }

}