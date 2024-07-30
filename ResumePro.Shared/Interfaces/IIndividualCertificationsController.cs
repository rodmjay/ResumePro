#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Interfaces;

public interface IIndividualCertificationsController
{
    Task<CertificationDto> Get(int certificationId);
    Task<List<CertificationDto>> Get();

    Task<ActionResult<CertificationDto>> CreateCertification(
        CertificationOptions options);

    Task<ActionResult<CertificationDto>> Update(int certificationId,
        CertificationOptions options);

    Task<Result> Delete(int certificationId);
}