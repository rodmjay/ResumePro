using System.Linq.Expressions;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Interfaces;

public interface ICountryStore
{
    Task<T> GetCountry<T>(string iso2) where T : CountryDto;

    Task<PagedList<T>> GetCountries<T>(Expression<Func<Country, bool>> predicate, PagingQuery paging)
        where T : CountryDto;
}