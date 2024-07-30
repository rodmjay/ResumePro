#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumeBuilder.Api.Controllers;

[Route("v1.0/skills/personal")]
public sealed class PersonSkillsController(IServiceProvider serviceProvider, IPersonaSkillService skillService)
    : BaseController(serviceProvider), IIndividualSkillsController
{
    [HttpGet]
    public async Task<List<PersonaSkillDto>> GetSkills()
    {
        return await skillService.GetPersonaSkills<PersonaSkillDto>(OrganizationId, UserId)
            .ConfigureAwait(false);
    }

    [HttpPatch("{skillId}")]
    public async Task<Result> ToggleSkill( [FromRoute] int skillId)
    {
        return await skillService.TogglePersonalSkill(OrganizationId, UserId, skillId)
            .ConfigureAwait(false);
    }
}