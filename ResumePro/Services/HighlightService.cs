#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using ResumePro.Extensions;
using ResumePro.Shared.Models;
using TechValley.Core.Builders;

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class HighlightService(IServiceProvider serviceProvider)
    : BaseService<Highlight>(serviceProvider), IHighlightService
{
    private IQueryable<Highlight> Highlights => Repository.Queryable();

    public Task<List<T>> GetHighlights<T>(int organizationId, int personId, int companyId, int positionId) where T : HighlightDto
    {
        return Highlights
            .AsNoTracking()
            .Where(Predicate(organizationId, personId, companyId, positionId))
            .OrderBy(x => x.Order)
            .ProjectTo<T>(Mapper)
            .ToListAsync();
    }

    public Task<T> GetHighlight<T>(int organizationId, int personId, int companyId, int positionId, int highlightId) where T : HighlightDto
    {
        return Highlights
            .AsNoTracking()
            .Where(Predicate(organizationId, personId, companyId, positionId))
            .Where(x => x.Id == highlightId)
            .ProjectTo<T>(Mapper)
            .FirstOrDefaultAsync();
    }

    public async Task<OneOf<HighlightDto, Result>> CreateHighlight(int organizationId, int personId, int companyId, int positionId,
        HighlightOptions options)
    {
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, CompanyId: {@companyId}, Options: {@options}"),
            organizationId, personId, companyId, positionId, options);

        var highlight = options
            .Create(async () => await NextId(organizationId, personId, companyId, positionId));

        var results = Repository.InsertOrUpdateGraph(highlight, true);
        if (results > 0)
            return await GetHighlight<HighlightDto>(organizationId, personId, companyId, positionId, highlight.Id);

        return Result.Failed();
    }


    public async Task<OneOf<HighlightDto, Result>> UpdateHighlight(int organizationId, int personId, int companyId, int positionId,
        int highlightId, HighlightOptions options)
    {
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, CompanyId: {@companyId}, PositionId: {@positionId}, HighlightId: {@highlightId}, Options: {@options}"),
            organizationId, personId, companyId, positionId, highlightId, options);

        var highlight = await Highlights
            .Where(Predicate(organizationId, personId, companyId, positionId))
            .Where(x => x.Id == highlightId)
            .FirstOrDefaultAsync();

        if (highlight == null) return Result.Failed();

        var highlights = await Highlights
            .Where(Predicate(organizationId, personId, companyId, positionId))
            .OrderBy(x => x.Order)
            .ToListAsync();

        highlight.Update(options, highlight.Order);


        highlights.Remove(highlight);

        highlight.Text = options.Text;
        highlight.ObjectState = ObjectState.Modified;

        var index = options.Order.GetValueOrDefault() - 1;

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
            return await GetHighlight<HighlightDto>(organizationId, personId, companyId, positionId, highlight.Id);

        return Result.Failed();
    }


    public async Task<Result> DeleteHighlight(int organizationId, int personId, int companyId, int positionId,
        int highlightId)
    {
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, CompanyId: {@companyId}, Position: {@positionId}, HighlightId: {@highlightId}"),
            organizationId, personId, companyId, positionId, highlightId);

        var highlight = await Highlights
            .Where(Predicate(organizationId, personId, companyId, positionId))
            .Where(x => x.Id == highlightId)
            .FirstOrDefaultAsync();

        if (highlight == null) return Result.Failed();

        var highlights = await Highlights
            .Where(Predicate(organizationId, personId, companyId, positionId))
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


    private async Task<int> NextId(int organizationId, int personId, int companyId, int positionId)
    {
        var id = await Highlights.AsNoTracking()
            .IgnoreQueryFilters()
            .Where(Predicate(organizationId, personId, companyId, positionId))
            .OrderByDescending(x => x.Id)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
        return id + 1;
    }


    private static Expression<Func<Highlight, bool>> Predicate(int organizationId, int personId, int companyId,
        int positionId)
    {
        Expression<Func<Highlight, bool>> predicate = PredicateBuilder.True<Highlight>()
            .And(x => x.OrganizationId == organizationId)
            .And(x => x.PersonId == personId)
            .And(x => x.CompanyId == companyId)
            .And(x => x.PositionId == positionId);

        return predicate;
    }
}