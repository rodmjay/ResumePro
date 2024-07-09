#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using ResumePro.Core.Services.Bases;
using ResumePro.Entities;
using ResumePro.Interfaces;
using ResumePro.Shared.Common;

namespace ResumePro.Services;

public class StateService : BaseService<StateProvince>, IStateService
{
    public StateService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    private IQueryable<StateProvince> States => Repository.Queryable();

    public Task<List<DropdownItem>> GetStatesDropdown(string countryId)
    {
        return States.AsNoTracking()
            .Where(x => x.Iso2 == countryId)
            .ProjectTo<DropdownItem>(Mapper)
            .ToListAsync();
    }
}