#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Proxies;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/jobs/{jobId}/highlights")]
public sealed class HighlightsController(IServiceProvider serviceProvider, IHighlightService highlightService)
    : BaseController(serviceProvider), IHighlightsController
{
    [HttpGet("{highlightId}")]
    public async Task<HighlightDto> GetHighlight([FromRoute] int personId, [FromRoute] int jobId,
        [FromRoute] int highlightId)
    {
        return await highlightService.GetHighlight<HighlightDto>(OrganizationId, highlightId, null)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<List<HighlightDto>> GetHighlights([FromRoute] int personId, [FromRoute] int jobId)
    {
        return await highlightService.GetHighlights<HighlightDto>(OrganizationId, jobId, null)
            .ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<ActionResult<HighlightDto>> CreateHighlight([FromRoute] int personId, [FromRoute] int jobId,
        [FromBody] CreateHighlightOptions options)
    {
        var result = await highlightService.CreateHighlight(OrganizationId, personId, jobId, null, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpPut("{highlightId}")]
    public async Task<ActionResult<HighlightDto>> UpdateHighlight([FromRoute] int personId, [FromRoute] int jobId,
        [FromRoute] int highlightId,
        [FromBody] HighlightOptions options)
    {
        var result = await highlightService
            .UpdateHighlight(OrganizationId, personId, jobId, null, highlightId, options)
            .ConfigureAwait(false);

        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpDelete("{highlightId}")]
    public async Task<Result> DeleteHighlight([FromRoute] int personId, [FromRoute] int jobId,
        [FromRoute] int highlightId)
    {
        return await highlightService.DeleteHighlight(OrganizationId, personId, jobId, null, highlightId)
            .ConfigureAwait(false);
    }
}