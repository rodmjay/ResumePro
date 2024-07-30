#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumeBuilder.Api.Controllers;

[Route("v1.0/languages")]
public sealed class PersonLanguagesController(IServiceProvider serviceProvider, IPersonaLanguageService languageService)
    : BaseController(serviceProvider), IIndividualLanguagesController
{
    [HttpGet]
    public async Task<List<PersonaLanguageDto>> GetPersonLanguages()
    {
        return await languageService.GetPersonaLanguages<PersonaLanguageDto>(OrganizationId, UserId)
            .ConfigureAwait(false);
    }
}