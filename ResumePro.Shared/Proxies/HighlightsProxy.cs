#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public sealed class HighlightsProxy(HttpClient httpClient) : BaseProxy(httpClient), IHighlightsController
{
    public async Task<HighlightDto> GetHighlight(int personId, int companyId, int positionId, int highlightId)
    {
        return await DoGet<HighlightDto>($"v1.0/people/{personId}/companies/{companyId}/positions/{positionId}/highlights/{highlightId}")
            .ConfigureAwait(false);
    }

    public async Task<List<HighlightDto>> GetHighlights(int personId, int companyId, int positionId)
    {
        return await DoGet<List<HighlightDto>>($"v1.0/people/{personId}/companies/{companyId}/positions/{positionId}/highlights")
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<HighlightDto>> CreateHighlight(int personId, int companyId, int positionId, HighlightOptions options)
    {
        return await DoPostActionResult<HighlightOptions, HighlightDto>(
                $"v1.0/people/{personId}/companies/{companyId}/positions/{positionId}/highlights", options)
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<HighlightDto>> UpdateHighlight(int personId, int companyId, int positionId, int highlightId,
        HighlightOptions options)
    {
        return await DoPutActionResult<HighlightOptions, HighlightDto>(
                $"v1.0/people/{personId}/companies/{companyId}/positions/{positionId}/highlights/{highlightId}", options)
            .ConfigureAwait(false);
    }

    public async Task<Result> DeleteHighlight(int personId, int companyId, int positionId, int highlightId)
    {
        return await DoDelete<Result>($"v1.0/people/{personId}/companies/{companyId}/positions/{positionId}/highlights/{highlightId}")
            .ConfigureAwait(false);
    }
}