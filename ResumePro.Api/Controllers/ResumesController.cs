#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Authorization;
using ResumePro.Core.Middleware.Bases;
using ResumePro.Generation;
using ResumePro.Interfaces;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/resumes")]
public sealed class ResumesController(IServiceProvider serviceProvider
    , IPdfStorage pdfStorage
    , IResumeService resumeService)
    : BaseController(serviceProvider), IResumeController
{
    [HttpGet("{resumeId}")]
    public async Task<ResumeDetails> GetResume([FromRoute] int personId, [FromRoute] int resumeId)
    {
        return await resumeService.GetResume<ResumeDetails>(OrganizationId, personId, resumeId)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<List<ResumeDto>> GetResumes([FromRoute] int personId)
    {
        return await resumeService.GetResumes<ResumeDto>(OrganizationId, personId)
            .ConfigureAwait(false);
    }

    [HttpGet("{resumeId}/generate")]
    public async Task<string> Generate([FromRoute] int personId, [FromRoute] int resumeId)
    {
        var resume = await resumeService.GetResume<ResumeDetails>(OrganizationId, personId, resumeId);
        var fileName = resume.GetFileName();
        
        var response = await resumeService.Generate2(resume)
            .ConfigureAwait(true);

        return await pdfStorage.SavePdfAsync(response, fileName);
    }

    [HttpGet("{resumeId}/pdf")]
    [AllowAnonymous]
    public async Task<IActionResult> PdfAnonymous([FromRoute] int personId, [FromRoute] int resumeId, [FromQuery]int organizationId)
    {
        var resume = await resumeService.GetResume<ResumeDetails>(organizationId, personId, resumeId);
        var fileName = resume.GetFileName();

        var resumeStream = await resumeService.Generate2(resume)
            .ConfigureAwait(true);

        return File(resumeStream, "application/pdf", fileName);
    }

    [HttpPost]
    public async Task<ActionResult<ResumeDetails>> CreateResume([FromRoute] int personId,
        [FromBody] ResumeOptions options)
    {
        var result = await resumeService.CreateResume(OrganizationId, personId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpPut("{resumeId}")]
    public async Task<ActionResult<ResumeDetails>> UpdateResume([FromRoute] int personId,
        [FromRoute] int resumeId,
        [FromBody] ResumeOptions options)
    {
        var result = await resumeService.UpdateResume(OrganizationId, personId, resumeId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpDelete("{resumeId}")]
    public Task<Result> DeleteResume([FromRoute] int personId,
        [FromRoute] int resumeId)
    {
        return resumeService.DeleteResume(OrganizationId, personId, resumeId);
    }
}