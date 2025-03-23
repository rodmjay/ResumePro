using ResumePro.Api.Interfaces;
using ResumePro.Services.Interfaces;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/companies/{companyId}/positions/{positionId}/highlights")]
public sealed class HighlightsController : BaseController, IHighlightsController
{
    private readonly IHighlightService _highlightService;

    public HighlightsController(IServiceProvider serviceProvider, IHighlightService highlightService) : base(
        serviceProvider)
    {
        _highlightService = highlightService;
    }

    [HttpGet("{highlightId}")]
    public async Task<HighlightDto> GetHighlight([FromRoute] int personId, [FromRoute] int companyId,
        [FromRoute] int positionId,
        [FromRoute] int highlightId)
    {
        return await _highlightService.GetHighlight<HighlightDto>(OrganizationId, companyId, positionId, highlightId)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<List<HighlightDto>> GetHighlights([FromRoute] int personId, [FromRoute] int companyId,
        [FromRoute] int positionId)
    {
        return await _highlightService.GetHighlights<HighlightDto>(OrganizationId, companyId, positionId, null)
            .ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<ActionResult<HighlightDto>> CreateHighlight([FromRoute] int personId, [FromRoute] int companyId,
        [FromRoute] int positionId,
        [FromBody] HighlightOptions options)
    {
        var result = await _highlightService
            .CreateHighlight(OrganizationId, personId, companyId, positionId, null, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }


    [HttpPut("{highlightId}")]
    public async Task<ActionResult<HighlightDto>> UpdateHighlight([FromRoute] int personId, [FromRoute] int companyId,
        [FromRoute] int positionId,
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
    public async Task<Result> DeleteHighlight([FromRoute] int personId, [FromRoute] int companyId,
        [FromRoute] int positionId,
        [FromRoute] int highlightId)
    {
        return await _highlightService
            .DeleteHighlight(OrganizationId, personId, companyId, positionId, null, highlightId)
            .ConfigureAwait(false);
    }
}