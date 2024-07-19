#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Services.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Interfaces;

public interface IPersonaLanguageService : IService<PersonaLanguage>
{
    Task<T> GetPersonaLanguage<T>(int organizationId, int personId, string language) where T : PersonaLanguageDto;


    Task<List<T>> GetPersonaLanguages<T>(int organizationId, int personId) where T : PersonaLanguageDto;

    Task<OneOf<PersonaLanguageDto, Result>> CreateOrUpdatePersonaLanguage(int organizationId, int personId,
        PersonLanguageOptions options);
}