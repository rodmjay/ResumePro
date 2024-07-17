#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Proxies;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/jobs/{jobId}/projects/{projectId}/highlights")]
public sealed class ProjectHighlightsController(IServiceProvider serviceProvider, IHighlightService highlightService)
    : BaseController(serviceProvider), IProjectHighlightsController
{
    [HttpGet("{highlightId}")]
    public async Task<HighlightDto> GetHighlight([FromRoute] int personId, [FromRoute] int jobId,
        [FromRoute] int projectId, [FromRoute] int highlightId)
    {
        return await highlightService.GetHighlight<HighlightDto>(OrganizationId, highlightId, projectId)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<List<HighlightDto>> GetHighlights([FromRoute] int personId, [FromRoute] int jobId,
        [FromRoute] int projectId)
    {
        return await highlightService.GetHighlights<HighlightDto>(OrganizationId, jobId, projectId)
            .ConfigureAwait(false);
    }


    [HttpPost]
    public async Task<ActionResult<HighlightDto>> CreateHighlight([FromRoute] int personId, [FromRoute] int jobId,
        [FromRoute] int projectId,
        [FromBody] CreateHighlightOptions options)
    {
        var result = await highlightService.CreateHighlight(OrganizationId, personId, jobId, null, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpPut("{highlightId}")]
    public async Task<ActionResult<HighlightDto>> UpdateHighlight([FromRoute] int personId, [FromRoute] int jobId,
        [FromRoute] int projectId,
        [FromRoute] int highlightId,
        [FromBody] HighlightOptions options)
    {
        var result = await highlightService
            .UpdateHighlight(OrganizationId, personId, jobId, projectId, highlightId, options)
            .ConfigureAwait(false);

        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpDelete("{highlightId}")]
    public async Task<Result> DeleteHighlight([FromRoute] int personId, [FromRoute] int jobId,
        [FromRoute] int projectId,
        [FromRoute] int highlightId)
    {
        return await highlightService.DeleteHighlight(OrganizationId, personId, jobId, projectId, highlightId)
            .ConfigureAwait(false);
    }
}