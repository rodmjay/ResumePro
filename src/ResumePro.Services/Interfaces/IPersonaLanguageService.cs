using Bespoke.Services.Interfaces;
using Bespoke.Shared.Common;
using OneOf;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Services.Interfaces;

public interface IPersonaLanguageService : IService<PersonaLanguage>
{
    Task<T> GetPersonaLanguage<T>(int organizationId, int personId, string language) where T : PersonaLanguageDto;


    Task<List<T>> GetPersonaLanguages<T>(int organizationId, int personId) where T : PersonaLanguageDto;

    Task<OneOf<PersonaLanguageDto, Result>> CreateOrUpdatePersonaLanguage(int organizationId, int personId,
        PersonLanguageOptions options);
        
    Task<Result> ToggleLanguage(int organizationId, int personId, int languageId, string proficiency);
}
