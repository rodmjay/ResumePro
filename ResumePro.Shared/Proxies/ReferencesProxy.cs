#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public class ReferencesProxy(HttpClient httpClient) : BaseProxy(httpClient), IReferencesController
{
    public async Task<ReferenceDto> Get(int personId, int referenceId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ReferenceDto>> GetReferences(int personId)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<ReferenceDto>> CreateReference(int personId, CreateReferenceOptions options)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<ReferenceDto>> UpdateReference(int personId, int referenceId, ReferenceOptions options)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> DeleteReference(int personId, int referenceId)
    {
        throw new NotImplementedException();
    }
}