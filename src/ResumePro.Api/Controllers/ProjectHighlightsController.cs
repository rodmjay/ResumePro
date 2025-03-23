using ResumePro.Api.Interfaces;
using ResumePro.Services.Interfaces;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/companies/{companyId}/positions/{positionId}/projects/{projectId}/highlights")]
public sealed class ProjectHighlightsController : BaseController, IProjectHighlightsController
{
    private readonly IHighlightService _highlightService;

    public ProjectHighlightsController(IServiceProvider serviceProvider, IHighlightService highlightService) : base(
        serviceProvider)
    {
        _highlightService = highlightService;
    }

    [HttpGet("{highlightId}")]
    public async Task<HighlightDto> GetHighlight([FromRoute] int personId, [FromRoute] int companyId,
        [FromRoute] int positionId,
        [FromRoute] int projectId, [FromRoute] int highlightId)
    {
        return await _highlightService.GetHighlight<HighlightDto>(OrganizationId, companyId, positionId, highlightId)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<List<HighlightDto>> GetHighlights([FromRoute] int personId, [FromRoute] int companyId,
        [FromRoute] int positionId,
        [FromRoute] int projectId)
    {
        return await _highlightService.GetHighlights<HighlightDto>(OrganizationId, companyId, positionId, projectId)
            .ConfigureAwait(false);
    }


    [HttpPost]
    public async Task<ActionResult<HighlightDto>> CreateHighlight([FromRoute] int personId, [FromRoute] int companyId,
        [FromRoute] int positionId,
        [FromRoute] int projectId,
        [FromBody] HighlightOptions options)
    {
        var result = await _highlightService
            .CreateHighlight(OrganizationId, personId, companyId, positionId, null, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpPut("{highlightId}")]
    public async Task<ActionResult<HighlightDto>> UpdateHighlight(
        [FromRoute] int personId,
        [FromRoute] int companyId,
        [FromRoute] int positionId,
        [FromRoute] int projectId,
        [FromRoute] int highlightId,
        [FromBody] HighlightOptions options)
    {
        var result = await _highlightService
            .UpdateHighlight(OrganizationId, personId, companyId, positionId, highlightId, options)
            .ConfigureAwait(false);

        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpDelete("{highlightId}")]
    public async Task<Result> DeleteHighlight(
        [FromRoute] int personId,
        [FromRoute] int companyId,
        [FromRoute] int positionId,
        [FromRoute] int projectId,
        [FromRoute] int highlightId)
    {
        return await _highlightService
            .DeleteHighlight(OrganizationId, personId, companyId, positionId, projectId, highlightId)
            .ConfigureAwait(false);
    }
}