#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Api.Controllers;

public sealed class SkillsController(IServiceProvider serviceProvider, ISkillService skillService)
    : BaseController(serviceProvider), ISkillsController
{
    [HttpGet]
    public async Task<List<SkillDto>> GetSkills()
    {
        return await skillService.GetSkills<SkillDto>()
            .ConfigureAwait(false);
    }
}