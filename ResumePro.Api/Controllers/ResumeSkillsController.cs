#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Common;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/resume/{resumeId}/skills")]
public class ResumeSkillsController : BaseController
{
    private readonly IResumeSkillService _resumeSkillService;

    public ResumeSkillsController(IServiceProvider serviceProvider, IResumeSkillService resumeSkillService) 
        : base(serviceProvider)
    {
        _resumeSkillService = resumeSkillService;
    }

    [HttpPatch("{skillId}")]
    public Task<Result> AddResumeSkill([FromRoute] int personId, [FromRoute] int resumeId, [FromRoute]int skillId)
    {
        return _resumeSkillService.AddResumeSkill(OrganizationId, personId, resumeId, skillId);
    }

    [HttpDelete("{skillId}")]
    public Task<Result> DeleteResumeSkill([FromRoute] int personId, [FromRoute] int resumeId, [FromRoute] int skillId)
    {
        return _resumeSkillService.DeleteResumeSkill(OrganizationId, personId, resumeId, skillId);

    }
}