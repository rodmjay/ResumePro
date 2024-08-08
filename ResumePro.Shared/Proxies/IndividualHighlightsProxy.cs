#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public sealed class IndividualHighlightsProxy(HttpClient httpClient)
    : BaseProxy(httpClient), IIndividualHighlightsController
{
    public async Task<HighlightDto> GetHighlight(int jobId, int highlightId)
    {
        return await DoGet<HighlightDto>($"v1.0/jobs/{jobId}/highlights/{highlightId}")
            .ConfigureAwait(false);
    }

    public async Task<List<HighlightDto>> GetHighlights(int jobId)
    {
        return await DoGet<List<HighlightDto>>($"v1.0/jobs/{jobId}/highlights")
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<HighlightDto>> CreateHighlight(int jobId, HighlightOptions options)
    {
        return await DoPostActionResult<HighlightOptions, HighlightDto>(
                $"v1.0/jobs/{jobId}/highlights", options)
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<HighlightDto>> UpdateHighlight(int jobId, int highlightId,
        HighlightOptions options)
    {
        return await DoPutActionResult<HighlightOptions, HighlightDto>(
                $"v1.0/jobs/{jobId}/highlights/{highlightId}", options)
            .ConfigureAwait(false);
    }

    public async Task<Result> DeleteHighlight(int jobId, int highlightId)
    {
        return await DoDelete<Result>($"v1.0/jobs/{jobId}/highlights/{highlightId}")
            .ConfigureAwait(false);
    }
}