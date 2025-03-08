#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Proxies;

public sealed class PersonLanguagesProxy : BaseProxy, IPersonLanguagesController
{
    public PersonLanguagesProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<List<PersonaLanguageDto>> GetPersonLanguages(int personId)
    {
        return await DoGet<List<PersonaLanguageDto>>($"v1.0/people/{personId}/languages")
            .ConfigureAwait(false);
    }
}