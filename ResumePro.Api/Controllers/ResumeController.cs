#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared;
using ResumePro.Shared.Options;

namespace ResumePro.Api.Controllers;

public class ResumeController : BaseController
{
    private readonly IResumeService _resumeService;

    public ResumeController(IServiceProvider serviceProvider, IResumeService resumeService) : base(serviceProvider)
    {
        _resumeService = resumeService;
    }

    [HttpGet("{resumeId}")]
    public Task<ResumeDetails> Get([FromRoute] int resumeId)
    {
        return _resumeService.GetResume<ResumeDetails>(OrganizationId, resumeId);
    }

    [HttpPost]
    public async Task<ActionResult<ResumeDetails>> CreateResume([FromQuery] int personId,
        [FromBody] CreateResumeOptions options)
    {
        var result = await _resumeService.CreateResume(OrganizationId, personId, options);
        if (result.IsT0)
        {
            return Ok(result.AsT0);
        }

        return BadRequest(result.AsT1);
    }
}