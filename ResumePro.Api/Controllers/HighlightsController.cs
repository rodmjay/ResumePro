#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared;
using ResumePro.Shared.Common;
using ResumePro.Shared.Options;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/jobs/{jobId}/highlights")]
public class HighlightsController : BaseController
{
    private readonly IHighlightService _highlightService;

    public HighlightsController(IServiceProvider serviceProvider, IHighlightService highlightService) : base(serviceProvider)
    {
        _highlightService = highlightService;
    }

    [HttpGet("{highlightId}")]
    public async Task <HighlightDto> GetHighlight([FromRoute] int personId, [FromRoute] int jobId, [FromRoute]int highlightId)
    {
        return await _highlightService.GetHighlight<HighlightDto>(OrganizationId, highlightId)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<List<HighlightDto>> GetHighlights([FromRoute] int personId, [FromRoute] int jobId)
    {
        return await _highlightService.GetHighlights<HighlightDto>(OrganizationId, jobId, null)
            .ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<ActionResult<HighlightDto>> CreateHighlight([FromRoute] int personId, [FromRoute] int jobId,
        [FromBody] CreateHighlightOptions options)
    {
        var result = await _highlightService.CreateHighlight(OrganizationId, personId, jobId, options)
            .ConfigureAwait(false);
        if (result.IsT0)
        {
            return Ok(result.AsT0);
        }

        return BadRequest(result.AsT1);
    }

    [HttpPut("{highlightId}")]
    public async Task<ActionResult<HighlightDto>> UpdateHighlight([FromRoute] int personId, [FromRoute] int jobId,
        [FromRoute] int highlightId,
        [FromBody] HighlightOptions options)
    {
        var result = await _highlightService.UpdateHighlight(OrganizationId, personId, jobId, highlightId, options)
            .ConfigureAwait(false);

        if (result.IsT0)
        {
            return Ok(result.AsT0);
        }

        return BadRequest(result.AsT1);
    }

    [HttpDelete("{highlightId}")]
    public async Task<Result> DeleteHighlight([FromRoute] int personId, [FromRoute] int jobId,
        [FromRoute] int highlightId)
    {
        return await _highlightService.DeleteHighlight(OrganizationId, personId, jobId, highlightId)
            .ConfigureAwait(false);
    }
}