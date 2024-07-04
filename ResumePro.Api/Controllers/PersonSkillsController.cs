#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared;
using ResumePro.Shared.Common;
using ResumePro.Shared.Options;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/skills")]
public class PersonSkillsController : BaseController
{
    private readonly IPersonalSkillsService _skillService;

    public PersonSkillsController(IServiceProvider serviceProvider, IPersonalSkillsService skillService) : base(serviceProvider)
    {
        _skillService = skillService;
    }

    [HttpGet]
    public Task<List<PersonaSkillDto>> GetSkills([FromRoute] int personId)
    {
        return _skillService.GetPersonaSkills<PersonaSkillDto>(OrganizationId, personId);
    }

    [HttpPatch]
    public Task<Result> AddOrUpdateSkill([FromRoute] int personId, [FromBody] PersonaSkillsOptions options)
    {
        return _skillService.AddOrUpdatePersonaSkill(OrganizationId, personId, options);
    }

    [HttpDelete("{skillId}")]
    public Task<Result> DeletePersonalSkill([FromRoute] int personId, [FromRoute] int skillId)
    {
        return _skillService.DeletePersonalSkill(OrganizationId, personId, skillId);
    }
}