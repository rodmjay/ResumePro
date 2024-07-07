#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OneOf;
using ResumePro.Core.Data.Enums;
using ResumePro.Core.Data.Interfaces;
using ResumePro.Core.Services.Bases;
using ResumePro.Entities;
using ResumePro.Interfaces;
using ResumePro.Shared;
using ResumePro.Shared.Common;
using ResumePro.Shared.Options;

namespace ResumePro.Services;

public class HighlightService : BaseService<Highlight>, IHighlightService
{
    private readonly IRepositoryAsync<Job> _jobRepository;

    public HighlightService(IServiceProvider serviceProvider, IRepositoryAsync<Job> jobRepository) : base(serviceProvider)
    {
        _jobRepository = jobRepository;
    }

    private IQueryable<Highlight> Highlights => Repository.Queryable();
    private IQueryable<Job> Jobs => _jobRepository.Queryable();

    public Task<List<T>> GetHighlights<T>(int organizationId, int jobId, int? projectId) where T : HighlightDto
    {
        return Highlights
            .AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.JobId == jobId && x.ProjectId == null)
            .OrderBy(x=>x.Order)
            .ProjectTo<T>(Mapper)
            .ToListAsync();
    }

    public Task<T> GetHighlight<T>(int organizationId, int highlightId, int? projectId) where T : HighlightDto
    {
        return Highlights
            .AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.Id == highlightId)
            .ProjectTo<T>(Mapper)
            .FirstOrDefaultAsync();
    }

    public async Task<OneOf<HighlightDto, Result>> CreateHighlight(int organizationId, int personId, int jobId, CreateHighlightOptions options)
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
            JobId = jobId,
            Id = await GetNextHighlightId(organizationId)
        };

        if (lastHighlight == null)
        {
            highlight.Order = 1;
        }
        else
        {
            highlight.Order = lastHighlight.Order + 1;
        }

        var results = Repository.InsertOrUpdateGraph(highlight, true);
        if (results > 0)
            return await GetHighlight<HighlightDto>(organizationId, highlight.Id, null);

        return Result.Failed();
    }

    public async Task<OneOf<HighlightDto, Result>> UpdateHighlight(int organizationId, int personId, int jobId, int highlightId, HighlightOptions options)
    {
        var highlight = await Highlights
            .Where(x => x.OrganizationId == organizationId && x.JobId == jobId && x.Id == highlightId)
            .FirstOrDefaultAsync();

        if (highlight == null)
        {
            return Result.Failed();
        }

        var highlights = await Highlights
            .Where(x => x.OrganizationId == organizationId && x.JobId == jobId && x.ProjectId == null)
            .OrderBy(x=>x.Order)
            .ToListAsync();

        highlights.Remove(highlight);

        highlight.Text = options.Text;
        highlight.ObjectState = ObjectState.Modified;

        int index = options.Order - 1;

        if (index < 0)
        {
            index = 0;
        }
        if (index > highlights.Count)
        {
            index = highlights.Count;
        }

        highlights.Insert(index, highlight);

        for (int i = 0; i < highlights.Count; i++)
        {
            highlights[i].Order = i + 1;
            highlights[i].ObjectState = ObjectState.Modified;

            Repository.InsertOrUpdateGraph(highlights[0], false);
        }

        var results = Repository.Commit();
        if (results > 0)
            return await GetHighlight<HighlightDto>(organizationId, highlight.Id, null);

        return Result.Failed();
    }

    public async Task<Result> DeleteHighlight(int organizationId, int personId, int jobId, int highlightId)
    {
        var highlight = await Highlights
            .Where(x => x.OrganizationId == organizationId && x.JobId == jobId && x.Id == highlightId)
            .FirstOrDefaultAsync();

        if (highlight == null)
        {
            return Result.Failed();
        }

        highlight.ObjectState = ObjectState.Deleted;

        var results = Repository.InsertOrUpdateGraph(highlight, true);
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