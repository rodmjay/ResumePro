#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/jobs/{jobId}/projects/{projectId}/highlights")]
public class ProjectHighlightsController : BaseController
{
    private readonly IHighlightService _highlightService;

    public ProjectHighlightsController(IServiceProvider serviceProvider, IHighlightService highlightService) : base(
        serviceProvider)
    {
        _highlightService = highlightService;
    }


    [HttpGet("{highlightId}")]
    public async Task<HighlightDto> GetHighlight([FromRoute] int personId, [FromRoute] int jobId,
        [FromRoute] int projectId, [FromRoute] int highlightId)
    {
        return await _highlightService.GetHighlight<HighlightDto>(OrganizationId, highlightId, projectId)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<List<HighlightDto>> GetHighlights([FromRoute] int personId, [FromRoute] int jobId,
        [FromRoute] int projectId)
    {
        return await _highlightService.GetHighlights<HighlightDto>(OrganizationId, jobId, projectId)
            .ConfigureAwait(false);
    }
}