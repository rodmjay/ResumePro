#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Shared.Proxies;

public sealed class PersonLanguagesProxy(HttpClient httpClient) : BaseProxy(httpClient), ILanguagesController
{
    public async Task<List<PersonaLanguageDto>> GetPersonLanguages(int personId)
    {
        return await DoGet<List<PersonaLanguageDto>>($"v1.0/people/{personId}/languages")
            .ConfigureAwait(false);
    }
}