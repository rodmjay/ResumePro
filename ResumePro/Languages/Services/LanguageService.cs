#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.Azure.Amqp.Serialization;
using ResumePro.Languages.Entities;
using ResumePro.Languages.Extensions;
using ResumePro.Languages.Interfaces;
using ResumePro.Languages.Models;
using ResumePro.Shared.Models;

namespace ResumePro.Languages.Services;

public class LanguageService(IServiceProvider serviceProvider)
    : BaseService<Language>(serviceProvider), ILanguageService
{
    private IQueryable<Language> Languages => Repository.Queryable();

    public async Task<PagedList<T>> GetLanguages<T>(LanguageFilters filters, PagingQuery query)
    {
        var expr = filters.GetExpression();
        return await this.PaginateAsync<Language, T>(expr, query);
    }

    public Task<T> GetLanguage<T>(string code2)
    {
        return Languages
            .AsNoTracking()
            .Where(x => x.Code2 == code2).ProjectTo<T>(Mapper).FirstAsync();
    }

    public async Task<List<LanguageDto>> GetLanguageDropdown()
    {
        return await Languages.AsNoTracking()
            .ProjectTo<LanguageDto>(Mapper)
            .ToListAsync();
    }
}