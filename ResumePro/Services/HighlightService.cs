#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using OneOf;
using ResumePro.Core.Data.Enums;
using ResumePro.Core.Services.Bases;
using ResumePro.Entities;
using ResumePro.Interfaces;
using ResumePro.Shared;
using ResumePro.Shared.Common;
using ResumePro.Shared.Options;

namespace ResumePro.Services;

public class HighlightService : BaseService<Highlight>, IHighlightService
{
    public HighlightService(IServiceProvider serviceProvider) : base(
        serviceProvider)
    {
    }

    private IQueryable<Highlight> Highlights => Repository.Queryable();

    public Task<List<T>> GetHighlights<T>(int organizationId, int jobId, int? projectId) where T : HighlightDto
    {
        return Highlights
            .AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.JobId == jobId && x.ProjectId == projectId)
            .OrderBy(x => x.Order)
            .ProjectTo<T>(Mapper)
            .ToListAsync();
    }

    public Task<T> GetHighlight<T>(int organizationId, int highlightId, int? projectId) where T : HighlightDto
    {
        return Highlights
            .AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.Id == highlightId && x.ProjectId == projectId)
            .ProjectTo<T>(Mapper)
            .FirstOrDefaultAsync();
    }

    public async Task<OneOf<HighlightDto, Result>> CreateHighlight(int organizationId, int personId, int jobId,
        int? projectId, CreateHighlightOptions options)
    {
        var lastHighlight = await
            Highlights.Where(x => x.OrganizationId == organizationId && x.JobId == jobId)
                .AsNoTracking()
                .OrderByDescending(x => x.Order)
                .FirstOrDefaultAsync();

        var highlight = new Highlight
        {
            ObjectState = ObjectState.Added,
            OrganizationId = organizationId,
            Text = options.Text,
            ProjectId = projectId,
            JobId = jobId,
            Id = await GetNextHighlightId(organizationId)
        };

        if (lastHighlight == null)
            highlight.Order = 1;
        else
            highlight.Order = lastHighlight.Order + 1;

        var results = Repository.InsertOrUpdateGraph(highlight, true);
        if (results > 0)
            return await GetHighlight<HighlightDto>(organizationId, highlight.Id, projectId);

        return Result.Failed();
    }

    public async Task<OneOf<HighlightDto, Result>> UpdateHighlight(int organizationId, int personId, int jobId,
        int? projectId,
        int highlightId, HighlightOptions options)
    {
        var highlight = await Highlights
            .Where(x => x.OrganizationId == organizationId && x.JobId == jobId && x.Id == highlightId && x.ProjectId == projectId)
            .FirstOrDefaultAsync();

        if (highlight == null) return Result.Failed();

        var highlights = await Highlights
            .Where(x => x.OrganizationId == organizationId && x.JobId == jobId && x.ProjectId == projectId)
            .OrderBy(x => x.Order)
            .ToListAsync();

        highlights.Remove(highlight);

        highlight.Text = options.Text;
        highlight.ObjectState = ObjectState.Modified;

        var index = options.Order - 1;

        if (index < 0) index = 0;
        if (index > highlights.Count) index = highlights.Count;

        highlights.Insert(index, highlight);

        for (var i = 0; i < highlights.Count; i++)
        {
            highlights[i].Order = i + 1;
            highlights[i].ObjectState = ObjectState.Modified;

            Repository.InsertOrUpdateGraph(highlights[0]);
        }

        var results = Repository.Commit();
        if (results > 0)
            return await GetHighlight<HighlightDto>(organizationId, highlight.Id, projectId);

        return Result.Failed();
    }

    public async Task<Result> DeleteHighlight(int organizationId, int personId, int jobId, int? projectId,
        int highlightId)
    {
        var highlight = await Highlights
            .Where(x => x.OrganizationId == organizationId && x.JobId == jobId && x.Id == highlightId &&
                        x.ProjectId == projectId)
            .FirstOrDefaultAsync();

        if (highlight == null) return Result.Failed();

        var highlights = await Highlights
            .Where(x => x.OrganizationId == organizationId && x.JobId == jobId && x.ProjectId == projectId)
            .OrderBy(x => x.Order)
            .ToListAsync();

        highlights.Remove(highlight);

        for (var i = 0; i < highlights.Count; i++)
        {
            highlights[i].Order = i + 1;
            highlights[i].ObjectState = ObjectState.Modified;

            Repository.InsertOrUpdateGraph(highlights[i]);
        }

        highlight.ObjectState = ObjectState.Deleted;
        Repository.InsertOrUpdateGraph(highlight);

        var results = Repository.Commit();
        if (results > 0)
            return Result.Success();

        return Result.Failed();
    }

    private async Task<int> GetNextHighlightId(int organizationId)
    {
        var highlight = await Highlights
            .IgnoreQueryFilters()
            .Where(x => x.OrganizationId == organizationId)
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync();

        if (highlight == null)
            return 1;

        return highlight.Id + 1;
    }
}