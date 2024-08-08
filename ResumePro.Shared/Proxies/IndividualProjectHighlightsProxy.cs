#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public sealed class IndividualProjectHighlightsProxy(HttpClient httpClient)
    : BaseProxy(httpClient), IIndividualProjectHighlightsController
{
    public async Task<HighlightDto> GetHighlight(int jobId, int projectId, int highlightId)
    {
        return await DoGet<HighlightDto>(
                $"v1.0/jobs/{jobId}/projects/{projectId}/highlights/{highlightId}")
            .ConfigureAwait(false);
    }

    public async Task<List<HighlightDto>> GetHighlights(int jobId, int projectId)
    {
        return await DoGet<List<HighlightDto>>(
                $"v1.0/jobs/{jobId}/projects/{projectId}/highlights")
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<HighlightDto>> CreateHighlight(int jobId, int projectId,
        HighlightOptions options)
    {
        return await DoPostActionResult<HighlightOptions, HighlightDto>(
                $"v1.0/jobs/{jobId}/projects/{projectId}/highlights", options)
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<HighlightDto>> UpdateHighlight(int jobId, int projectId,
        int highlightId, HighlightOptions options)
    {
        return await DoPutActionResult<HighlightOptions, HighlightDto>(
                $"v1.0/jobs/{jobId}/projects/{projectId}/highlights/{highlightId}", options)
            .ConfigureAwait(false);
    }

    public async Task<Result> DeleteHighlight(int jobId, int projectId, int highlightId)
    {
        return await DoDelete<Result>(
                $"v1.0/jobs/{jobId}/projects/{projectId}/highlights/{highlightId}")
            .ConfigureAwait(false);
    }
}