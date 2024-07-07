#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Services.Interfaces;
using ResumePro.Entities;
using ResumePro.Shared;
using ResumePro.Shared.Common;
using ResumePro.Shared.Options;
using OneOf;

namespace ResumePro.Interfaces;

public interface IHighlightService : IService<Highlight>
{
    Task<List<T>> GetHighlights<T>(int organizationId, int jobId, int? projectId) where T : HighlightDto;

    Task<T> GetHighlight<T>(int organizationId, int highlightId, int? projectId) where T : HighlightDto;

    Task<OneOf<HighlightDto, Result>> CreateHighlight(int organizationId, int personId, int jobId,
        CreateHighlightOptions options);
    Task<OneOf<HighlightDto, Result>> UpdateHighlight(int organizationId, int personId, int jobId, int highlightId, HighlightOptions options);

    Task<Result> DeleteHighlight(int organizationId, int personId, int jobId, int highlightId);
}