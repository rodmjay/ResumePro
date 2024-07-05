#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
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
    public Task <HighlightDto> GetHighlight([FromRoute] int personId, [FromRoute] int jobId, [FromRoute]int highlightId)
    {
        return _highlightService.GetHighlight<HighlightDto>(OrganizationId, highlightId);
    }

    [HttpGet]
    public Task<List<HighlightDto>> GetHighlights([FromRoute] int personId, [FromRoute] int jobId)
    {
        return _highlightService.GetHighlights<HighlightDto>(OrganizationId, jobId);
    }

    [HttpPost]
    public async Task<ActionResult<HighlightDto>> CreateHighlight([FromRoute] int personId, [FromRoute] int jobId,
        [FromBody] HighlightOptions options)
    {
        var result = await _highlightService.CreateHighlight(OrganizationId, personId, jobId, options);
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
        var result = await _highlightService.UpdateHighlight(OrganizationId, personId, jobId, highlightId, options);
        if (result.IsT0)
        {
            return Ok(result.AsT0);
        }

        return BadRequest(result.AsT1);
    }

    [HttpDelete("{highlightId}")]
    public Task<Result> DeleteHighlight([FromRoute] int personId, [FromRoute] int jobId,
        [FromRoute] int highlightId)
    {
        return _highlightService.DeleteHighlight(OrganizationId, personId, jobId, highlightId);
    }
}