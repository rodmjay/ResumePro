#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.Net.Http.Headers;
using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;

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

    [HttpGet("{resumeId}/Download")]
    public async Task<IActionResult> Download([FromRoute] int personId, [FromRoute] int resumeId)
    {
        var filePath = await _resumeService.SaveResumeAsPdf(OrganizationId, personId, resumeId);
        var fileName = Path.GetFileName(filePath);

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound();
        }

        var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
        var fileStream = new MemoryStream(fileBytes);

        var contentType = "application/pdf";

        var contentDisposition = new ContentDispositionHeaderValue("attachment")
        {
            FileName = fileName
        };

        Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

        return File(fileStream, contentType, fileName);
    }

    [HttpGet]
    public async Task<List<ResumeDto>> GetResumes([FromRoute] int personId, [FromRoute] int resumeId)
    {
        return await _resumeService.GetResumes<ResumeDto>(OrganizationId, personId)
            .ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<ActionResult<ResumeDetails>> CreateResume([FromRoute] int personId,
        [FromBody] ResumeOptions options)
    {
        var result = await _resumeService.CreateResume(OrganizationId, personId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpPut("{resumeId}")]
    public async Task<ActionResult<ResumeDetails>> UpdateResume([FromRoute] int personId,
        [FromRoute] int resumeId,
        [FromBody] ResumeOptions options)
    {
        var result = await _resumeService.UpdateResume(OrganizationId, personId, resumeId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpDelete("{resumeId}")]
    public Task<Result> DeleteResume([FromRoute] int personId,
        [FromRoute] int resumeId)
    {
        return _resumeService.DeleteResume(OrganizationId, personId, resumeId);
    }
}