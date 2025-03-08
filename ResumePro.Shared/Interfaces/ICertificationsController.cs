#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;

namespace ResumePro.Shared.Interfaces;

public interface ICertificationsController
{
    Task<CertificationDto> Get(int personId, int certificationId);
    Task<List<CertificationDto>> Get(int personId);

    Task<ActionResult<CertificationDto>> CreateCertification(int personId,
        CertificationOptions options);

    Task<ActionResult<CertificationDto>> Update(int personId, int certificationId,
        CertificationOptions options);

    Task<Result> Delete(int personId, int certificationId);
}