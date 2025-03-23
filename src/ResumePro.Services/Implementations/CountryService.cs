using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Bespoke.Data.Extensions;
using Bespoke.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using ResumePro.Services.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Implementations;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class CountryService : BaseService<Country>, ICountryService
{
    private readonly IRepositoryAsync<StateProvince> _stateProvinceRepo;

    public CountryService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _stateProvinceRepo = UnitOfWork.RepositoryAsync<StateProvince>();
    }

    public IQueryable<Country> Countries => Repository.Queryable().Include(x => x.StateProvinces);
    public IQueryable<StateProvince> StateProvinces => _stateProvinceRepo.Queryable();

    public Task<T> GetCountry<T>(string id) where T : CountryDto
    {
        return Countries.Where(x => x.Iso2 == id)
            .AsNoTracking()
            .ProjectTo<T>(Mapper)
            .AsNoTracking()
            .FirstAsync();
    }

    public Task<PagedList<T>> GetCountries<T>(Expression<Func<Country, bool>> predicate, PagingQuery paging)
        where T : CountryDto
    {
        return this.PaginateAsync<Country, T>(predicate, paging);
    }

    public Task<List<T>> GetStateProvincesForCountry<T>(string iso2) where T : StateProvinceOutput
    {
        return StateProvinces.Where(x => x.Iso2 == iso2).ProjectTo<T>(Mapper)
            .AsNoTracking()
            .ToListAsync();
    }
}