#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/languages")]
public sealed class PersonLanguagesController(IServiceProvider serviceProvider, IPersonaLanguageService languageService)
    : BaseController(serviceProvider), IPersonLanguagesController
{
    [HttpGet]
    public async Task<List<PersonaLanguageDto>> GetPersonLanguages([FromRoute] int personId)
    {
        return await languageService.GetPersonaLanguages<PersonaLanguageDto>(OrganizationId, personId)
            .ConfigureAwait(false);
    }
}