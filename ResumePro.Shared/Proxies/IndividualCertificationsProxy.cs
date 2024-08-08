#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public sealed class IndividualCertificationsProxy(HttpClient client)
    : BaseProxy(client), IIndividualCertificationsController
{
    public async Task<CertificationDto> Get(int certificationId)
    {
        return await DoGet<CertificationDto>($"v1.0/certifications/{certificationId}")
            .ConfigureAwait(false);
    }

    public async Task<List<CertificationDto>> Get()
    {
        return await DoGet<List<CertificationDto>>($"v1.0/certifications")
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<CertificationDto>> CreateCertification(CertificationOptions options)
    {
        return await DoPostActionResult<CertificationOptions, CertificationDto>(
                $"v1.0/certifications", options)
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<CertificationDto>> Update(int certificationId,
        CertificationOptions options)
    {
        return await DoPutActionResult<CertificationOptions, CertificationDto>(
                $"v1.0/certifications/{certificationId}", options)
            .ConfigureAwait(false);
    }

    public async Task<Result> Delete(int certificationId)
    {
        return await DoDelete<Result>($"v1.0/certifications/{certificationId}")
            .ConfigureAwait(false);
    }
}