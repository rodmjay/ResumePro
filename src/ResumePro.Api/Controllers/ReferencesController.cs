using ResumePro.Api.Interfaces;
using ResumePro.Services.Interfaces;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/references")]
public sealed class ReferencesController : BaseController, IReferencesController
{
    private readonly IReferenceService _referenceService;

    public ReferencesController(IServiceProvider serviceProvider, IReferenceService referenceService) : base(
        serviceProvider)
    {
        _referenceService = referenceService;
    }

    [HttpGet("{referenceId}")]
    public async Task<ReferenceDto> Get([FromRoute] int personId, [FromRoute] int referenceId)
    {
        return await _referenceService.GetReference<ReferenceDto>(OrganizationId, personId, referenceId)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<List<ReferenceDto>> GetReferences([FromRoute] int personId)
    {
        return await _referenceService.GetReferences<ReferenceDto>(OrganizationId, personId)
            .ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<ActionResult<ReferenceDto>> CreateReference([FromRoute] int personId,
        [FromBody] ReferenceOptions options)
    {
        var result = await _referenceService.CreateReference(OrganizationId, personId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpPut("{referenceId}")]
    public async Task<ActionResult<ReferenceDto>> UpdateReference([FromRoute] int personId,
        [FromRoute] int referenceId,
        [FromBody] ReferenceOptions options)
    {
        var result = await _referenceService.UpdateReference(OrganizationId, personId, referenceId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpDelete("{referenceId}")]
    public Task<Result> DeleteReference([FromRoute] int personId,
        [FromRoute] int referenceId)
    {
        return _referenceService.DeleteReference(OrganizationId, personId, referenceId);
    }
}