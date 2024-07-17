#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public class ProjectHighlightsProxy(HttpClient httpClient) : BaseProxy(httpClient), IProjectHighlightsController
{
    public async Task<HighlightDto> GetHighlight(int personId, int jobId, int projectId, int highlightId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<HighlightDto>> GetHighlights(int personId, int jobId, int projectId)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<HighlightDto>> CreateHighlight(int personId, int jobId, int projectId, HighlightCreateOptions options)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<HighlightDto>> UpdateHighlight(int personId, int jobId, int projectId, int highlightId, HighlightUpdateOptions options)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> DeleteHighlight(int personId, int jobId, int projectId, int highlightId)
    {
        throw new NotImplementedException();
    }
}