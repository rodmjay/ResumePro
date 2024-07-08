#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Linq.Expressions;
using ResumePro.Entities;
using ResumePro.Shared;
using ResumePro.Shared.Common;

namespace ResumePro.Interfaces;

public interface ICountryStore
{
    Task<T> GetCountry<T>(string iso2) where T : CountryDto;

    Task<PagedList<T>> GetCountries<T>(Expression<Func<Country, bool>> predicate, PagingQuery paging)
        where T : CountryDto;
}