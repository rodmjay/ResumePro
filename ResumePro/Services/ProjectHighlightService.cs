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
public sealed class ProjectHighlightService(IServiceProvider serviceProvider)
    : BaseService<ProjectHighlight>(serviceProvider), IProjectHighlightService
{
    private IQueryable<ProjectHighlight> Highlights => Repository.Queryable();


    private static Expression<Func<ProjectHighlight, bool>> Predicate(int organizationId, int personId, int companyId,
        int positionId, int projectHighlight)
    {
        Expression<Func<ProjectHighlight, bool>> predicate = PredicateBuilder.True<ProjectHighlight>()
            .And(x => x.OrganizationId == organizationId)
            .And(x => x.PersonId == personId)
            .And(x => x.CompanyId == companyId)
            .And(x => x.PositionId == positionId)
            .And(x => x.ProjectId == projectHighlight);

        return predicate;
    }
    private async Task<int> NextId(int organizationId, int personId, int companyId, int positionId, int projectId)
    {
        var id = await Highlights.AsNoTracking()
            .IgnoreQueryFilters()
            .Where(Predicate(organizationId, personId, companyId, positionId, projectId))
            .OrderByDescending(x => x.Id)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
        return id + 1;
    }
    public Task<List<T>> GetAsync<T>(int organizationId, int personId, int companyId, int positionId, int projectId) where T : HighlightDto
    {
        return Highlights
            .AsNoTracking()
            .Where(Predicate(organizationId, personId, companyId, positionId, projectId))
            .OrderBy(x => x.Order)
            .ProjectTo<T>(Mapper)
            .ToListAsync();
    }

    public Task<T> GetAsync<T>(int organizationId, int personId, int companyId, int positionId, int projectId, int highlightId) where T : HighlightDto
    {
        return Highlights
            .AsNoTracking()
            .Where(Predicate(organizationId, personId, companyId, positionId, projectId))
            .Where(x => x.Id == highlightId)
            .ProjectTo<T>(Mapper)
            .FirstOrDefaultAsync();
    }

    public async Task<Result> DeleteAsync(int organizationId, int personId, int companyId, int positionId, int projectId,
        int highlightId)
    {
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, CompanyId: {@companyId}, Position: {@positionId}, Project: {@projectId}, HighlightId: {@highlightId}"),
            organizationId, personId, companyId, positionId, projectId, highlightId);

        var highlight = await Highlights
            .Where(Predicate(organizationId, personId, companyId, positionId, projectId))
            .Where(x => x.Id == highlightId)
            .FirstOrDefaultAsync();

        if (highlight == null) return Result.Failed();

        var highlights = await Highlights
            .Where(Predicate(organizationId, personId, companyId, positionId, projectId))
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

    public async Task<OneOf<HighlightDto, Result>> CreateAsync(int organizationId, int personId, int companyId, int positionId, int projectId,
        HighlightOptions options)
    {
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, CompanyId: {@companyId}, PositionId: {@positionId}, Project: {@projectId}, Options: {@options}"),
            organizationId, personId, companyId, positionId, projectId, options);

        var highlight = options
            .CreateProjectHighlight(async () => await NextId(organizationId, personId, companyId, positionId, projectId));

        var results = Repository.InsertOrUpdateGraph(highlight, true);
        if (results > 0)
            return await GetAsync<HighlightDto>(organizationId, personId, companyId, positionId, projectId, highlight.Id);

        return Result.Failed();
    }

    public async Task<OneOf<HighlightDto, Result>> UpdateAsync(int organizationId, int personId, int companyId, int positionId, int projectId,
        int highlightId, HighlightOptions options)
    {
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, CompanyId: {@companyId}, PositionId: {@positionId}, HighlightId: {@highlightId}, Options: {@options}"),
            organizationId, personId, companyId, positionId, highlightId, options);

        var highlight = await Highlights
            .Where(Predicate(organizationId, personId, companyId, positionId, projectId))
            .Where(x => x.Id == highlightId)
            .FirstOrDefaultAsync();

        if (highlight == null) return Result.Failed();

        var highlights = await Highlights
            .Where(Predicate(organizationId, personId, companyId, positionId, projectId))
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
            return await GetAsync<HighlightDto>(organizationId, personId, companyId, positionId, projectId, highlight.Id);

        return Result.Failed();
    }
}