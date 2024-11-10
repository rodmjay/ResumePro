#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Azure.Storage.Blobs.Models;
using ResumePro.Core.Builders;
using ResumePro.Entities;
using ResumePro.Extensions;
using ResumePro.Shared.Models;

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class PositionService(
    IServiceProvider serviceProvider,
    IRepositoryAsync<Highlight> highlightRepo,
    IRepositoryAsync<Company> companyRepo)
    : BaseService<Position>(serviceProvider), IPositionService
{
    private IQueryable<Position> Positions => Repository.Queryable();
    private IQueryable<Company> Companies => companyRepo.Queryable();
    private IQueryable<Highlight> Highlights => highlightRepo.Queryable();

    public async Task<T> GetPosition<T>(int organizationId, int personId, int companyId, int positionId) where T : PositionDto
    {
        return await Positions.AsNoTracking().Where(x =>
                x.OrganizationId == organizationId && x.CompanyId == companyId && x.Id == positionId &&
                x.PersonId == personId)
            .ProjectTo<T>(Mapper).FirstOrDefaultAsync();
    }

    public async Task<List<T>> GetPositions<T>(int organizationId, int personId, int companyId) where T : PositionDto
    {
        return await Positions.AsNoTracking().Where(x =>
                x.OrganizationId == organizationId && x.CompanyId == companyId &&
                x.PersonId == personId)
            .ProjectTo<T>(Mapper).ToListAsync();
    }

    public async Task<OneOf<PositionDetails, Result>> CreatePosition(int organizationId, int personId, int companyId, PositionOptions options)
    {
        var position = new Position
        {
            Id = await GetNextPositionId(organizationId, personId, companyId),
            PersonId = personId,
            OrganizationId = organizationId,
            CompanyId = companyId,
            EndDate = options.EndDate,
            StartDate = options.StartDate,
            JobTitle = options.JobTitle,
            ObjectState = ObjectState.Added
        };

        for (var index = 0; index < options.Highlights.Count; index++)
        {
            var option = options.Highlights[index];
            var highlight = new Highlight()
            {
                OrganizationId = organizationId,
                Id = index + 1,
                ObjectState = ObjectState.Added,
                Order = index + 1,
                Text = option.Text
            };

            position.Highlights.Add(highlight);
        }

        for (var index = 0; index < options.Projects.Count; index++)
        {
            var option = options.Projects[index];
            var project = new Project()
            {
                OrganizationId = organizationId,
                Id = index + 1,
                ObjectState = ObjectState.Added,
                Order = index + 1,
                Name = option.Name
            };

            for (var j = 0; j < option.Highlights.Count; j++)
            {
                var highlightOption = option.Highlights[j];

                var highlight = new ProjectHighlight()
                {
                    OrganizationId = organizationId,
                    Id = j + 1,
                    ObjectState = ObjectState.Added,
                    Order = j + 1,
                    Text = highlightOption.Text
                };

                project.Highlights.Add(highlight);

            }

            position.Projects.Add(project);
        }

        var records = Repository.InsertOrUpdateGraph(position, true);

        if (records > 0)
        {
            return await GetPosition<PositionDetails>(organizationId, personId, companyId, position.Id);
        }

        return Result.Failed();
    }

    public async Task<OneOf<PositionDetails, Result>> UpdatePosition(int organizationId, int personId, int companyId, int positionId, PositionOptions options)
    {
        var position = await Positions
            .Where(PositionHelpers.GetPredicate(organizationId, personId, companyId, positionId))
            .FirstOrDefaultAsync();

        position.JobTitle = options.JobTitle;
        position.StartDate = options.StartDate;
        position.EndDate = options.EndDate;
        position.ObjectState = ObjectState.Modified;

        var records = await Repository.UpdateAsync(position, true);

        if (records > 0)
            return Result.Success();

        return Result.Failed();
    }

    public async Task<Result> DeletePosition(int organizationId, int personId, int companyId, int positionId)
    {
        var success = await Repository
            .DeleteAsync(PositionHelpers.GetPredicate(organizationId, personId, companyId, positionId));

        return success ? Result.Success(positionId) : Result.Failed();
    }

    private async Task<int> GetNextPositionId(int organizationId, int personId, int companyId)
    {
        var id = await Positions.OrderByDescending(x => x.Id)
            .AsNoTracking()
            .Where(PositionHelpers.GetPredicate(organizationId, personId, companyId))
            .Select(x => x.Id)
            .FirstOrDefaultAsync();

        return id + 1;
    }

    private async Task<int> GetNextHighlightId(int organizationId, int personId, int companyId, int positionId)
    {
        var predicate = PredicateBuilder.True<Highlight>()
            .And(x => x.OrganizationId == organizationId)
            .And(x => x.PersonId == personId)
            .And(x => x.CompanyId == companyId)
            .And(x => x.PositionId == positionId);

        var id = await Highlights.AsNoTracking()
            .IgnoreQueryFilters()
            .Where(predicate)
            .OrderByDescending(x => x.Id)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
        return id + 1;
    }
}