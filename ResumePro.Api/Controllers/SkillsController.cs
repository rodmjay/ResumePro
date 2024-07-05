#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared;

namespace ResumePro.Api.Controllers;

public class SkillsController : BaseController
{
    private readonly ISkillService _skillService;

    public SkillsController(IServiceProvider serviceProvider, ISkillService skillService) : base(serviceProvider)
    {
        _skillService = skillService;
    }

    [HttpGet]
    public async Task<List<SkillDto>> GetSkills()
    {
        return await _skillService.GetSkills<SkillDto>()
            .ConfigureAwait(false);
    }
}