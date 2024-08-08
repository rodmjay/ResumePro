#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Shared.Proxies;

public sealed class PersonProxy(HttpClient httpClient) : BaseProxy(httpClient), IPersonController
{
    public async Task<PersonaDetails> GetPerson()
    {
        return await DoGet<PersonaDetails>($"v1.0/person")
            .ConfigureAwait(false);
    }
}