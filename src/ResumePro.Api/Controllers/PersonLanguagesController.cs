using ResumePro.Api.Interfaces;
using ResumePro.Services.Interfaces;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/languages")]
public sealed class PersonLanguagesController : BaseController, IPersonLanguagesController
{
    private readonly IPersonaLanguageService _languageService;

    public PersonLanguagesController(IServiceProvider serviceProvider, IPersonaLanguageService languageService) : base(
        serviceProvider)
    {
        _languageService = languageService;
    }

    [HttpGet]
    public async Task<List<PersonaLanguageDto>> GetPersonLanguages([FromRoute] int personId)
    {
        return await _languageService.GetPersonaLanguages<PersonaLanguageDto>(OrganizationId, personId)
            .ConfigureAwait(false);
    }
    
    [HttpPost("{languageId}/{proficiency}")]
    public async Task<Result> ToggleLanguage([FromRoute] int personId, [FromRoute] int languageId, [FromRoute] string proficiency)
    {
        return await _languageService.ToggleLanguage(OrganizationId, personId, languageId, proficiency)
            .ConfigureAwait(false);
    }
}
