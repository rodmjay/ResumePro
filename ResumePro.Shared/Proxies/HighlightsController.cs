#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public class HighlightsController : BaseProxy, IHighlightsController
{
    public HighlightsController(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<HighlightDto> GetHighlight(int personId, int jobId, int highlightId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<HighlightDto>> GetHighlights(int personId, int jobId)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<HighlightDto>> CreateHighlight(int personId, int jobId, CreateHighlightOptions options)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<HighlightDto>> UpdateHighlight(int personId, int jobId, int highlightId, HighlightOptions options)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> DeleteHighlight(int personId, int jobId, int highlightId)
    {
        throw new NotImplementedException();
    }
}