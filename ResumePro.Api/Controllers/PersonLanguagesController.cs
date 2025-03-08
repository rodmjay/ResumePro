#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Services.Interfaces;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/languages")]
public sealed class PersonLanguagesController : BaseController, IPersonLanguagesController
{
    private readonly IPersonaLanguageService _languageService;

    public PersonLanguagesController(IServiceProvider serviceProvider, IPersonaLanguageService languageService) : base(serviceProvider)
    {
        _languageService = languageService;
    }

    [HttpGet]
    public async Task<List<PersonaLanguageDto>> GetPersonLanguages([FromRoute] int personId)
    {
        return await _languageService.GetPersonaLanguages<PersonaLanguageDto>(OrganizationId, personId)
            .ConfigureAwait(false);
    }
}