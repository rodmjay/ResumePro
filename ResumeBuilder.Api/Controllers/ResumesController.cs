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

namespace ResumeBuilder.Api.Controllers;

[Route("v1.0/resumes")]
public sealed class ResumesController(IServiceProvider serviceProvider
    , IPdfStorage pdfStorage
    , IResumeService resumeService)
    : BaseController(serviceProvider), IIndividualResumeController
{
    [HttpGet("{resumeId}")]
    public async Task<ResumeDetails> GetResume([FromRoute] int resumeId)
    {
        return await resumeService.GetResume<ResumeDetails>(OrganizationId, UserId, resumeId)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<List<ResumeDto>> GetResumes()
    {
        return await resumeService.GetResumes<ResumeDto>(OrganizationId, UserId)
            .ConfigureAwait(false);
    }

    [HttpGet("{resumeId}/generate")]
    public async Task<string> Generate([FromRoute] int resumeId)
    {
        var resume = await resumeService.GetResume<ResumeDetails>(OrganizationId, UserId, resumeId);
        var fileName = resume.GetFileName();
        
        var response = await resumeService.Generate2(resume)
            .ConfigureAwait(true);

        return await pdfStorage.SavePdfAsync(response, fileName);
    }

    [HttpGet("{resumeId}/pdf")]
    [AllowAnonymous]
    public async Task<IActionResult> PdfAnonymous([FromRoute] int resumeId, [FromQuery]int organizationId)
    {
        var resume = await resumeService.GetResume<ResumeDetails>(organizationId, UserId, resumeId);
        var fileName = resume.GetFileName();

        var resumeStream = await resumeService.Generate2(resume)
            .ConfigureAwait(true);
        
        Response.Headers.Add("Content-Disposition", $"inline; filename={fileName}");

        return File(resumeStream, "application/pdf");
    }

    [HttpPost]
    public async Task<ActionResult<ResumeDetails>> CreateResume(
        [FromBody] ResumeOptions options)
    {
        var result = await resumeService.CreateResume(OrganizationId, UserId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpPut("{resumeId}")]
    public async Task<ActionResult<ResumeDetails>> UpdateResume(
        [FromRoute] int resumeId,
        [FromBody] ResumeOptions options)
    {
        var result = await resumeService.UpdateResume(OrganizationId, UserId, resumeId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpDelete("{resumeId}")]
    public Task<Result> DeleteResume(
        [FromRoute] int resumeId)
    {
        return resumeService.DeleteResume(OrganizationId, UserId, resumeId);
    }
}