using ResumePro.Api.Interfaces;
using ResumePro.Services.Interfaces;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/resumes")]
public sealed class ResumesController : BaseController, IResumeController
{
    private readonly IResumeService _resumeService;

    public ResumesController(IServiceProvider serviceProvider
        , IResumeService resumeService) : base(serviceProvider)
    {
        _resumeService = resumeService;
    }

    [HttpGet("{resumeId}")]
    public async Task<ResumeDetails> GetResume([FromRoute] int personId, [FromRoute] int resumeId)
    {
        return await _resumeService.GetResume<ResumeDetails>(OrganizationId, personId, resumeId)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<List<ResumeDto>> GetResumes([FromRoute] int personId)
    {
        return await _resumeService.GetResumes<ResumeDto>(OrganizationId, personId)
            .ConfigureAwait(false);
    }

    //[HttpGet("{resumeId}/generate")]
    //public async Task<string> Generate([FromRoute] int personId, [FromRoute] int resumeId)
    //{
    //    var resume = await _resumeService.GetResume<ResumeDetails>(OrganizationId, personId, resumeId);
    //    var fileName = resume.GetFileName();

    //    var response = await _resumeService.Generate2(resume)
    //        .ConfigureAwait(true);

    //    return await _pdfStorage.SavePdfAsync(response, fileName);
    //}

    //[HttpGet("{resumeId}/pdf")]
    //[AllowAnonymous]
    //public async Task<IActionResult> PdfAnonymous([FromRoute] int personId, [FromRoute] int resumeId, [FromQuery]int organizationId)
    //{
    //    var resume = await _resumeService.GetResume<ResumeDetails>(organizationId, personId, resumeId);
    //    var fileName = resume.GetFileName();

    //    var resumeStream = await _resumeService.Generate2(resume)
    //        .ConfigureAwait(true);

    //    Response.Headers.Add("Content-Disposition", $"inline; filename={fileName}");

    //    return File(resumeStream, "application/pdf");
    //}

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