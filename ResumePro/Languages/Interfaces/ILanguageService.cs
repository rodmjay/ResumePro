#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Services.Interfaces;
using ResumePro.Languages.Entities;
using ResumePro.Languages.Models;
using ResumePro.Shared.Common;

namespace ResumePro.Languages.Interfaces;

public interface ILanguageService : IService<Language>
{
    Task<PagedList<T>> GetLanguages<T>(LanguageFilters filters, PagingQuery query);
    Task<T> GetLanguage<T>(string code2);
}