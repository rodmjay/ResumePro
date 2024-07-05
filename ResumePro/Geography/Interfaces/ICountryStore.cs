#region Header

// /*

// Author: Rod Johnson, Architect, rodmjay@gmail.com
// */

#endregion

using System.Linq.Expressions;
using ResumePro.Geography.Entities;
using ResumePro.Geography.Models;
using ResumePro.Shared.Common;

namespace ResumePro.Geography.Interfaces
{
    public interface ICountryStore
    {
        Task<T> GetCountry<T>(string iso2) where T : CountryOutput;

        Task<PagedList<T>> GetCountries<T>(Expression<Func<Country, bool>> predicate, PagingQuery paging)
            where T : CountryOutput;
    }
}