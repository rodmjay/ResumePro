#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Models;

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class StateService(IServiceProvider serviceProvider)
    : BaseService<StateProvince>(serviceProvider), IStateService
{
    private IQueryable<StateProvince> States => Repository.Queryable();

    public Task<List<StateProvinceOutput>> GetStatesDropdown(string countryId)
    {
        return States.AsNoTracking()
            .Where(x => x.Iso2 == countryId)
            .ProjectTo<StateProvinceOutput>(Mapper)
            .ToListAsync();
    }
}