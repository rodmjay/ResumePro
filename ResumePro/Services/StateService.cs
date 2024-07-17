#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class StateService(IServiceProvider serviceProvider)
    : BaseService<StateProvince>(serviceProvider), IStateService
{
    private IQueryable<StateProvince> States => Repository.Queryable();

    public Task<List<DropdownItem>> GetStatesDropdown(string countryId)
    {
        return States.AsNoTracking()
            .Where(x => x.Iso2 == countryId)
            .ProjectTo<DropdownItem>(Mapper)
            .ToListAsync();
    }
}