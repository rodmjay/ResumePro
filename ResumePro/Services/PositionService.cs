#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Extensions;
using ResumePro.Shared.Models;
using System.Linq.Expressions;
using TechValley.Core.Builders;

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class PositionService(
    IServiceProvider serviceProvider,
    IRepositoryAsync<Highlight> highlightRepo,
    IRepositoryAsync<ProjectHighlight> projectHighlightRepo,
    IRepositoryAsync<Company> companyRepo)
    : BaseService<Position>(serviceProvider), IPositionService
{
    private IQueryable<Position> Positions => Repository.Queryable();
    private IQueryable<Company> Companies => companyRepo.Queryable();
    private IQueryable<Highlight> Highlights => highlightRepo.Queryable();
    private IQueryable<ProjectHighlight> ProjectHighlights => projectHighlightRepo.Queryable();

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

    public async Task<OneOf<PositionDetails, Result>> AddPosition(int organizationId, int personId, int companyId, PositionOptions options)
    {
        var position = options.Create(async () => await NextId(organizationId, personId, companyId));

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
            .Include(x => x.Projects)
            .ThenInclude(x => x.Highlights)
            .Include(x => x.Highlights)
            .Where(Predicate(organizationId, personId, companyId, positionId))
            .FirstOrDefaultAsync();

        if (position == null)
        {
            return Result.Failed();
        }

        position.Update(options);

        var records = await Repository.UpdateAsync(position, true);

        if (records > 0)
            return await GetPosition<PositionDetails>(organizationId, personId, companyId, positionId);

        return Result.Failed();
    }

    public async Task<Result> DeletePosition(int organizationId, int personId, int companyId, int positionId)
    {
        var success = await Repository
            .DeleteAsync(Predicate(organizationId, personId, companyId, positionId));

        return success ? Result.Success(positionId) : Result.Failed();
    }

    private async Task<int> NextId(int organizationId, int personId, int companyId)
    {
        var id = await Positions.OrderByDescending(x => x.Id)
            .AsNoTracking()
            .Where(Predicate(organizationId, personId, companyId))
            .Select(x => x.Id)
            .FirstOrDefaultAsync();

        return id + 1;
    }

    private static Expression<Func<Position, bool>> Predicate(int organizationId, int personId, int companyId)
    {
        var predicate = PredicateBuilder.True<Position>()
            .And(x => x.OrganizationId == organizationId)
            .And(x => x.PersonId == personId)
            .And(x => x.CompanyId == companyId);

        return predicate;
    }

    private static Expression<Func<Position, bool>> Predicate(int organizationId, int personId, int companyId, int positionId)
    {
        var predicate = Predicate(organizationId, personId, companyId)
            .And(x => x.Id == positionId);

        return predicate;
    }
}