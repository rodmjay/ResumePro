﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using ResumePro.Core.Data.Interfaces;
using ResumePro.Core.Services.Bases;
using ResumePro.Entities;
using ResumePro.Interfaces;
using ResumePro.Shared;
using ResumePro.Shared.Common;

namespace ResumePro.Services;

public class CountryService : BaseService<Country>, ICountryService
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
            .FirstOrDefaultAsync();
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

    private string GetLogMessage(string message, [CallerMemberName] string callerName = null)
    {
        return $"[{nameof(CountryService)}.{callerName}] - {message}";
    }
}