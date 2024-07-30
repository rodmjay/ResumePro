#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Interfaces;

public interface IIndividualProjectHighlightsController
{
    Task<HighlightDto> GetHighlight(int jobId,
        int projectId, int highlightId);

    Task<List<HighlightDto>> GetHighlights(int jobId,
        int projectId);

    Task<ActionResult<HighlightDto>> CreateHighlight(int jobId,
        int projectId,
        HighlightOptions options);

    Task<ActionResult<HighlightDto>> UpdateHighlight(int jobId,
        int projectId,
        int highlightId,
        HighlightOptions options);

    Task<Result> DeleteHighlight(int jobId,
        int projectId,
        int highlightId);
}