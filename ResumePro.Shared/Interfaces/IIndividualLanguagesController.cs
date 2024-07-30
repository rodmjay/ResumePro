using ResumePro.Shared.Models;

namespace ResumePro.Shared.Interfaces;

public interface IIndividualLanguagesController
{
    Task<List<PersonaLanguageDto>> GetPersonLanguages();
}