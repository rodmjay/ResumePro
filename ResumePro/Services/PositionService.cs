#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Azure.Storage.Blobs.Models;
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

        for (var index = 0; index < options.HighlightOptions.Count; index++)
        {
            var option = options.HighlightOptions[index];
            var highlight = new Highlight()
            {
                OrganizationId = organizationId,
                Id = index+1,
                ObjectState = ObjectState.Added,
                Order = index+1,
                Text = option.Text
            };

            position.Highlights.Add(highlight);
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
        throw new NotImplementedException();
    }

    public async Task<Result> DeletePosition(int organizationId, int personId, int companyId, int positionId)
    {
        throw new NotImplementedException();
    }

    private async Task<int> GetNextPositionId(int organizationId, int personId, int companyId)
    {
        var id = await Positions.OrderByDescending(x => x.Id)
            .AsNoTracking()
            .Where(x=>x.OrganizationId == organizationId && x.PersonId == personId && x.CompanyId == companyId)
            .Select(x=>x.Id)
            .FirstOrDefaultAsync();

        return id + 1;
    }


    private async Task<int> GetNextHighlightId(int organizationId, int personId, int companyId, int positionId)
    {
        var id = await Highlights.AsNoTracking()
            .IgnoreQueryFilters()
            .Where(x => x.OrganizationId == organizationId 
                        && x.PersonId == personId 
                        && x.CompanyId == companyId 
                        && x.PositionId == positionId)
            .OrderByDescending(x => x.Id)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
        return id + 1;
    }
}