﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Services.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Interfaces;

public interface IHighlightService : IService<Highlight>
{
    Task<List<T>> GetHighlights<T>(int organizationId, int jobId, int? projectId) where T : HighlightDto;

    Task<T> GetHighlight<T>(int organizationId, int highlightId, int? projectId) where T : HighlightDto;

    Task<OneOf<HighlightDto, Result>> CreateHighlight(int organizationId, int personId, int jobId,
        int? projectId,
        HighlightOptions options);

    Task<OneOf<HighlightDto, Result>> UpdateHighlight(int organizationId, int personId, int jobId, int? projectId,
        int highlightId,
        HighlightOptions options);

    Task<Result> DeleteHighlight(int organizationId, int personId, int jobId, int? projectId, int highlightId);
}