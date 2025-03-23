using System.Diagnostics.CodeAnalysis;
using Bespoke.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using ResumePro.Services.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Implementations;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class StateService : BaseService<StateProvince>, IStateService
{
    public StateService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    private IQueryable<StateProvince> States => Repository.Queryable();

    public Task<List<StateProvinceOutput>> GetStatesDropdown(string countryId)
    {
        return States.AsNoTracking()
            .Where(x => x.Iso2 == countryId)
            .ProjectTo<StateProvinceOutput>(Mapper)
            .ToListAsync();
    }
}