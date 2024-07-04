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
    Task<T> GetHighlight<T>(int organizationId, int highlightId) where T : HighlightDto;

    Task<OneOf<HighlightDto, Result>> CreateHighlight(int organizationId, int personId, int jobId, CreateHighlightOptions options);
}