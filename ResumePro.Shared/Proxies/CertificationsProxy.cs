#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public class CertificationsProxy(HttpClient httpClient) : BaseProxy(httpClient), ICertificationsController
{
    public async Task<CertificationDto> Get(int personId, int certificationId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<CertificationDto>> Get(int personId)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<CertificationDto>> Create(int personId, CertificationOptions options)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<CertificationDto>> Update(int personId, int certificationId, CertificationOptions options)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> Delete(int personId, int certificationId)
    {
        throw new NotImplementedException();
    }
}