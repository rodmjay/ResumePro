#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Services.Interfaces;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Api.Controllers;

public sealed class SkillsController : BaseController, ISkillsController
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