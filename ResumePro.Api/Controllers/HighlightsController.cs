#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared;
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

    [HttpPost]
    public async Task<ActionResult<HighlightDto>> CreateHighlight([FromRoute] int personId, [FromRoute] int jobId,
        [FromBody] CreateHighlightOptions options)
    {
        var result = await _highlightService.CreateHighlight(OrganizationId, personId, jobId, options);
        if (result.IsT0)
        {
            return Ok(result.AsT0);
        }

        return BadRequest(result.AsT1);
    }
}