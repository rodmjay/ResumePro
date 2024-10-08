﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Services.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Interfaces;

public interface ICertificationService : IService<Certification>
{
    Task<List<T>> GetCertifications<T>(int organizationId, int personId) where T : CertificationDto;
    Task<T> GetCertification<T>(int organizationId, int personId, int certificationId) where T : CertificationDto;

    Task<OneOf<CertificationDto, Result>> CreateCertification(int organizationId, int personId,
        CertificationOptions options);

    Task<OneOf<CertificationDto, Result>> UpdateCertification(int organizationId, int personId, int certificationId,
        CertificationOptions options);

    Task<Result> DeleteCertification(int organizationId, int personId, int certificationId);
}