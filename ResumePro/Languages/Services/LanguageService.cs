#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using ResumePro.Core.Services.Bases;
using ResumePro.Languages.Entities;
using ResumePro.Languages.Extensions;
using ResumePro.Languages.Interfaces;
using ResumePro.Languages.Models;
using ResumePro.Shared.Common;

namespace ResumePro.Languages.Services;

public class LanguageService : BaseService<Language>, ILanguageService
{
    public LanguageService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    private IQueryable<Language> Languages => Repository.Queryable();

    public async Task<PagedList<T>> GetLanguages<T>(LanguageFilters filters, PagingQuery query)
    {
        var expr = filters.GetExpression();
        return await this.PaginateAsync<Language, T>(expr, query);
    }

    public Task<T> GetLanguage<T>(string code2)
    {
        return Languages.Where(x => x.Code2 == code2).ProjectTo<T>(Mapper).FirstAsync();
    }
}