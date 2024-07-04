#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper.QueryableExtensions;
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
    public HighlightService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    private IQueryable<Highlight> Highlights => Repository.Queryable();

    public Task<T> GetHighlight<T>(int organizationId, int highlightId) where T : HighlightDto
    {
        return Highlights
            .AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.Id == highlightId)
            .ProjectTo<T>(ProjectionMapping)
            .FirstOrDefaultAsync();
    }

    public async Task<OneOf<HighlightDto, Result>> CreateHighlight(int organizationId, int personId, int jobId, CreateHighlightOptions options)
    {
        // todo: figure out ordering of other highlights

        var highlight = new Highlight
        {
            ObjectState = ObjectState.Added,
            OrganizationId = organizationId,
            Order = options.Order,
            Text = options.Text,
            JobId = jobId,
            Id = await GetNextHighlightId(organizationId)
        };

        var results = Repository.InsertOrUpdateGraph(highlight, true);
        if (results > 0)
            return await GetHighlight<HighlightDto>(organizationId, highlight.Id);

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