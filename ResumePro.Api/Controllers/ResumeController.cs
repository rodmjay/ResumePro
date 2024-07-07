#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared;
using ResumePro.Shared.Common;
using ResumePro.Shared.Options;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/resumes")]
public class ResumeController : BaseController
{
    private readonly IResumeService _resumeService;

    public ResumeController(IServiceProvider serviceProvider, IResumeService resumeService) : base(serviceProvider)
    {
        _resumeService = resumeService;
    }

    [HttpGet("{resumeId}")]
    public async Task<ResumeDetails> Get([FromRoute] int personId, [FromRoute] int resumeId)
    {
        return await _resumeService.GetResume<ResumeDetails>(OrganizationId, personId, resumeId)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<List<ResumeDto>> GetResumes([FromRoute] int personId, [FromRoute] int resumeId)
    {
        return await _resumeService.GetResumes<ResumeDto>(OrganizationId, personId)
            .ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<ActionResult<ResumeDetails>> CreateResume([FromRoute] int personId,
        [FromBody] CreateResumeOptions options)
    {
        var result = await _resumeService.CreateResume(OrganizationId, personId, options)
            .ConfigureAwait(false);
        if (result.IsT0)
        {
            return Ok(result.AsT0);
        }

        return BadRequest(result.AsT1);
    }

    [HttpPut("{resumeId}")]
    public async Task<ActionResult<ResumeDetails>> UpdateResume([FromRoute] int personId,
        [FromRoute] int resumeId,
        [FromBody] CreateResumeOptions options)
    {
        var result = await _resumeService.UpdateResume(OrganizationId, personId, resumeId, options)
            .ConfigureAwait(false);
        if (result.IsT0)
        {
            return Ok(result.AsT0);
        }

        return BadRequest(result.AsT1);
    }

    [HttpDelete("{resumeId}")]
    public Task<Result> DeleteResume([FromRoute] int personId,
        [FromRoute] int resumeId)
    {
        return _resumeService.DeleteResume(OrganizationId, personId, resumeId);
    }
}