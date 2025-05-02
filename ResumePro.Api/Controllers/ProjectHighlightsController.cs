#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/companies/{companyId}/positions/{positionId}/projects/{projectId}/highlights")]
public sealed class ProjectHighlightsController(IServiceProvider serviceProvider, IProjectHighlightService highlightService)
    : ExtendedBaseController(serviceProvider), IProjectHighlightsController
{
    [HttpGet("{highlightId}")]
    public async Task<HighlightDto> GetHighlight([FromRoute] int personId, [FromRoute] int companyId, [FromRoute]int positionId,
        [FromRoute] int projectId, [FromRoute] int highlightId)
    {
        return await highlightService.GetAsync<HighlightDto>(OrganizationId, companyId, positionId, positionId, projectId, highlightId)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<List<HighlightDto>> GetHighlights([FromRoute] int personId, [FromRoute] int companyId, [FromRoute]int positionId,
        [FromRoute] int projectId)
    {
        return await highlightService.GetAsync<HighlightDto>(OrganizationId, personId, companyId, positionId, projectId)
            .ConfigureAwait(false);
    }


    [HttpPost]
    public async Task<ActionResult<HighlightDto>> CreateHighlight([FromRoute] int personId, [FromRoute] int companyId, [FromRoute]int positionId,
        [FromRoute] int projectId,
        [FromBody] HighlightOptions options)
    {
        var result = await highlightService.CreateAsync(OrganizationId, personId, companyId, positionId, projectId, options)
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
        var result = await highlightService
            .UpdateAsync(OrganizationId, personId, companyId, positionId, projectId, highlightId, options)
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
        return await highlightService.DeleteAsync(OrganizationId, personId, companyId, positionId, projectId, highlightId)
            .ConfigureAwait(false);
    }
}