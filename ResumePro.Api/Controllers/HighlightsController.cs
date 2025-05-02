#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/companies/{companyId}/positions/{positionId}/highlights")]
public sealed class HighlightsController(IServiceProvider serviceProvider, IHighlightService highlightService)
    : ExtendedBaseController(serviceProvider), IHighlightsController
{
    [HttpGet("{highlightId}")]
    public async Task<HighlightDto> GetHighlight([FromRoute] int personId, [FromRoute] int companyId, [FromRoute] int positionId,
        [FromRoute] int highlightId)
    {
        return await highlightService.GetHighlight<HighlightDto>(OrganizationId, personId, companyId, positionId, highlightId)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<List<HighlightDto>> GetHighlights([FromRoute] int personId, [FromRoute] int companyId, [FromRoute]int positionId)
    {
        return await highlightService.GetHighlights<HighlightDto>(OrganizationId, personId, companyId, positionId)
            .ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<ActionResult<HighlightDto>> CreateHighlight([FromRoute] int personId, [FromRoute] int companyId, [FromRoute]int positionId,
        [FromBody] HighlightOptions options)
    {
        var result = await highlightService.CreateHighlight(OrganizationId, personId, companyId, positionId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }


    [HttpPut("{highlightId}")]
    public async Task<ActionResult<HighlightDto>> UpdateHighlight([FromRoute] int personId, [FromRoute] int companyId, [FromRoute] int positionId,
        [FromRoute] int highlightId,
        [FromBody] HighlightOptions options)
    {
        var result = await highlightService
            .UpdateHighlight(OrganizationId, personId, companyId, positionId, highlightId, options)
            .ConfigureAwait(false);

        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpDelete("{highlightId}")]
    public async Task<Result> DeleteHighlight([FromRoute] int personId, [FromRoute] int companyId, [FromRoute]int positionId,
        [FromRoute] int highlightId)
    {
        return await highlightService.DeleteHighlight(OrganizationId, personId, companyId, positionId, highlightId)
            .ConfigureAwait(false);
    }
}