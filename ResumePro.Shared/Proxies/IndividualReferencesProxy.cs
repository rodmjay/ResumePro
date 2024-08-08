#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public sealed class IndividualReferencesProxy(HttpClient httpClient)
    : BaseProxy(httpClient), IIndividualReferencesController
{
    public async Task<ReferenceDto> Get( int referenceId)
    {
        return await DoGet<ReferenceDto>($"v1.0/references/{referenceId}")
            .ConfigureAwait(false);
    }

    public async Task<List<ReferenceDto>> GetReferences()
    {
        return await DoGet<List<ReferenceDto>>($"v1.0/references")
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<ReferenceDto>> CreateReference( ReferenceOptions options)
    {
        return await DoPostActionResult<ReferenceOptions, ReferenceDto>($"v1.0/references",
            options).ConfigureAwait(false);
    }

    public async Task<ActionResult<ReferenceDto>> UpdateReference( int referenceId,
        ReferenceOptions options)
    {
        return await DoPutActionResult<ReferenceOptions, ReferenceDto>(
                $"v1.0/references/{referenceId}", options)
            .ConfigureAwait(false);
    }

    public async Task<Result> DeleteReference( int referenceId)
    {
        return await DoDelete<Result>($"v1.0/references/{referenceId}")
            .ConfigureAwait(false);
    }
}