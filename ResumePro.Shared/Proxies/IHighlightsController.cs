#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public interface IHighlightsController
{
    Task<HighlightDto> GetHighlight( int personId,  int jobId,
         int highlightId);

    Task<List<HighlightDto>> GetHighlights( int personId,  int jobId);

    Task<ActionResult<HighlightDto>> CreateHighlight( int personId,  int jobId,
         CreateHighlightOptions options);

    Task<ActionResult<HighlightDto>> UpdateHighlight( int personId,  int jobId,
         int highlightId,
         HighlightOptions options);

    Task<Result> DeleteHighlight( int personId,  int jobId,
         int highlightId);
}