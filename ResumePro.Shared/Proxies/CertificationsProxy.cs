#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;

namespace ResumePro.Shared.Proxies;

public sealed class CertificationsProxy : BaseProxy, ICertificationsController
{
    public CertificationsProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<CertificationDto> Get(int personId, int certificationId)
    {
        return await DoGet<CertificationDto>($"v1.0/people/{personId}/certifications/{certificationId}")
            .ConfigureAwait(false);
    }

    public async Task<List<CertificationDto>> Get(int personId)
    {
        return await DoGet<List<CertificationDto>>($"v1.0/people/{personId}/certifications")
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<CertificationDto>> CreateCertification(int personId, CertificationOptions options)
    {
        return await DoPostActionResult<CertificationOptions, CertificationDto>(
                $"v1.0/people/{personId}/certifications", options)
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<CertificationDto>> Update(int personId, int certificationId,
        CertificationOptions options)
    {
        return await DoPutActionResult<CertificationOptions, CertificationDto>(
                $"v1.0/people/{personId}/certifications/{certificationId}", options)
            .ConfigureAwait(false);
    }

    public async Task<Result> Delete(int personId, int certificationId)
    {
        return await DoDelete<Result>($"v1.0/people/{personId}/certifications/{certificationId}")
            .ConfigureAwait(false);
    }
}