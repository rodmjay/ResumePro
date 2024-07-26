#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public sealed class ReferencesProxy(HttpClient httpClient) : BaseProxy(httpClient), IReferencesController
{
    public async Task<ReferenceDto> Get(int personId, int referenceId)
    {
        return await DoGet<ReferenceDto>($"v1.0/people/{personId}/references/{referenceId}")
            .ConfigureAwait(false);
    }

    public async Task<List<ReferenceDto>> GetReferences(int personId)
    {
        return await DoGet<List<ReferenceDto>>($"v1.0/people/{personId}/references")
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<ReferenceDto>> CreateReference(int personId, ReferenceOptions options)
    {
        return await DoPostActionResult<ReferenceOptions, ReferenceDto>($"v1.0/people/{personId}/references",
            options).ConfigureAwait(false);
    }

    public async Task<ActionResult<ReferenceDto>> UpdateReference(int personId, int referenceId,
        ReferenceOptions options)
    {
        return await DoPutActionResult<ReferenceOptions, ReferenceDto>(
                $"v1.0/people/{personId}/references/{referenceId}", options)
            .ConfigureAwait(false);
    }

    public async Task<Result> DeleteReference(int personId, int referenceId)
    {
        return await DoDelete<Result>($"v1.0/people/{personId}/references/{referenceId}")
            .ConfigureAwait(false);
    }
}