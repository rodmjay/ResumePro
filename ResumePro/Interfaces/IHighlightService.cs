#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Models;

namespace ResumePro.Interfaces;

public interface IProjectHighlightService : IService<ProjectHighlight>
{
    Task<List<T>> GetAsync<T>(int organizationId, int personId, int companyId, int positionId, int projectId) where T : HighlightDto;

    Task<T> GetAsync<T>(int organizationId, int personId, int companyId, int positionId, int projectId, int highlightId) where T : HighlightDto;

    Task<Result> DeleteAsync(int organizationId, int personId, int companyId, int positionId, int projectId, int highlightId);
    Task<OneOf<HighlightDto, Result>> CreateAsync(int organizationId, int personId, int companyId, int positionId, int projectId,
        HighlightOptions options);

    Task<OneOf<HighlightDto, Result>> UpdateAsync(int organizationId, int personId, int companyId, int positionId, int projectId,
        int highlightId,
        HighlightOptions options);
}

public interface IHighlightService : IService<Highlight>
{
    Task<List<T>> GetHighlights<T>(int organizationId, int personId, int companyId, int positionId) where T : HighlightDto;

    Task<T> GetHighlight<T>(int organizationId, int personId, int companyId, int positionId, int highlightId) where T : HighlightDto;

    Task<OneOf<HighlightDto, Result>> CreateHighlight(int organizationId, int personId, int companyId, int positionId,
        HighlightOptions options);

    Task<OneOf<HighlightDto, Result>> UpdateHighlight(int organizationId, int personId, int companyId, int positionId,
        int highlightId,
        HighlightOptions options);


    Task<Result> DeleteHighlight(int organizationId, int personId, int companyId, int positionId, int highlightId);
}