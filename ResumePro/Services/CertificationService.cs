﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ResumePro.Shared.Models;

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class CertificationService(IServiceProvider serviceProvider, CertificationErrorDescriber errors)
    : BaseService<Certification>(serviceProvider), ICertificationService
{
    private IQueryable<Certification> Certifications => Repository.Queryable();

    public Task<List<T>> GetCertifications<T>(int organizationId, int personId) where T : CertificationDto
    {
        return Certifications.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.PersonId == personId)
            .ProjectTo<T>(Mapper)
            .ToListAsync();
    }

    public Task<T> GetCertification<T>(int organizationId, int personId, int certificationId) where T : CertificationDto
    {
        return Certifications.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.PersonId == personId && x.Id == certificationId)
            .ProjectTo<T>(Mapper)
            .FirstOrDefaultAsync();
    }

    public async Task<OneOf<CertificationDto, Result>> CreateCertification(int organizationId, int personId,
        CertificationOptions options)
    {
        Logger.LogInformation(
            GetLogMessage("OrganizationId: {@organizationId}, PersonId: {@personId}, Options: {@options}"),
            organizationId,
            personId, options);

        var certification = new Certification
        {
            ObjectState = ObjectState.Added,
            OrganizationId = organizationId,
            Id = await GetNextCertificationId(organizationId, personId),
            Body = options.Body,
            PersonId = personId,
            Date = options.Date.Value,
            Name = options.Name
        };

        var records = Repository.InsertOrUpdateGraph(certification, true);
        if (records > 0) return await GetCertification<CertificationDto>(organizationId, personId, certification.Id);

        return Result.Failed(errors.UnableToSaveCertification());
    }

    public async Task<OneOf<CertificationDto, Result>> UpdateCertification(int organizationId, int personId,
        int certificationId, CertificationOptions options)
    {
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, CertificationId: {@certificationId}, Options: {@options}"),
            organizationId, personId, certificationId, options);

        var certification = await Certifications.Where(x =>
                x.OrganizationId == organizationId && x.PersonId == personId && x.Id == certificationId)
            .FirstOrDefaultAsync();

        if (certification == null)
            return Result.Failed(errors.CertificationNotFound(certificationId));

        certification.ObjectState = ObjectState.Modified;
        certification.Date = options.Date.Value;
        certification.Body = options.Body;
        certification.Name = options.Name;

        var records = Repository.InsertOrUpdateGraph(certification, true);
        if (records > 0) return await GetCertification<CertificationDto>(organizationId, personId, certification.Id);

        return Result.Failed(errors.UnableToSaveCertification());
    }

    public async Task<Result> DeleteCertification(int organizationId, int personId, int certificationId)
    {
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, CertificationId: {@certificationId}"),
            organizationId, personId, certificationId);

        var certification = await Certifications.Where(x =>
                x.OrganizationId == organizationId && x.PersonId == personId && x.Id == certificationId)
            .FirstOrDefaultAsync();

        if (certification == null)
            return Result.Failed(errors.CertificationNotFound(certificationId));

        certification.ObjectState = ObjectState.Deleted;

        var records = Repository.InsertOrUpdateGraph(certification, true);
        if (records > 0) return Result.Success();

        return Result.Failed();
    }

    private async Task<int> GetNextCertificationId(int organizationId, int personId)
    {
        var id = await Certifications.AsNoTracking()
            .IgnoreQueryFilters()
            .Where(x => x.OrganizationId == organizationId && x.PersonId == personId)
            .OrderByDescending(x => x.Id)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
        return id + 1;
    }
}