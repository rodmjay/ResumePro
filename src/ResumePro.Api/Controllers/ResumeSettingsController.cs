using ResumePro.Api.Interfaces;
using ResumePro.Services.Interfaces;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/resumes/{resumeId}/settings")]
public sealed class ResumeSettingsController : BaseController, IResumeSettingsController
{
    private readonly IResumeSettingsService _service;

    public ResumeSettingsController(IServiceProvider serviceProvider, IResumeSettingsService service) : base(
        serviceProvider)
    {
        _service = service;
    }

    [HttpPut]
    public async Task<ActionResult<ResumeSettingsDto>> UpdateSettings(
        [FromRoute] int personId, [FromRoute] int resumeId,
        [FromBody] ResumeSettingsOptions options)
    {
        var result = await _service.AddOrUpdateUpdateResumeSettings(OrganizationId, personId, resumeId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }
}