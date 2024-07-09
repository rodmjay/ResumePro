#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using OneOf;
using ResumePro.Core.Data.Enums;
using ResumePro.Core.Services.Bases;
using ResumePro.Entities;
using ResumePro.Interfaces;
using ResumePro.Shared;
using ResumePro.Shared.Common;
using ResumePro.Shared.Options;

namespace ResumePro.Services;

public class CertificationService : BaseService<Certification>, ICertificationService
{
    public CertificationService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    private IQueryable<Certification> Certifications => Repository.Queryable();

    public Task<List<T>> GetCertifications<T>(int organizationId, int personId) where T : CertificationDto
    {
        return Certifications.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.PersonaId == personId)
            .ProjectTo<T>(Mapper)
            .ToListAsync();
    }

    public Task<T> GetCertification<T>(int organizationId, int personId, int certificationId) where T : CertificationDto
    {
        return Certifications.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.PersonaId == personId && x.Id == certificationId)
            .ProjectTo<T>(Mapper)
            .FirstOrDefaultAsync();
    }

    public async Task<OneOf<CertificationDto, Result>> CreateCertification(int organizationId, int personId,
        CertificationOptions options)
    {
        var certification = new Certification
        {
            ObjectState = ObjectState.Added,
            OrganizationId = organizationId,
            Id = await GetNextCertificationId(organizationId, personId),
            Body = options.Body,
            PersonaId = personId,
            Date = options.Date,
            Name = options.Name
        };

        var records = Repository.InsertOrUpdateGraph(certification, true);
        if (records > 0) return await GetCertification<CertificationDto>(organizationId, personId, certification.Id);

        return Result.Failed();
    }

    public async Task<OneOf<CertificationDto, Result>> UpdateCertification(int organizationId, int personId,
        int certificationId, CertificationOptions options)
    {
        var certification = await Certifications.Where(x =>
                x.OrganizationId == organizationId && x.PersonaId == personId && x.Id == certificationId)
            .FirstOrDefaultAsync();

        if (certification == null)
            return Result.Failed();

        certification.ObjectState = ObjectState.Modified;
        certification.Date = options.Date;
        certification.Body = options.Body;
        certification.Name = options.Name;

        var records = Repository.InsertOrUpdateGraph(certification, true);
        if (records > 0) return await GetCertification<CertificationDto>(organizationId, personId, certification.Id);

        return Result.Failed();
    }

    public async Task<Result> DeleteCertification(int organizationId, int personId, int certificationId)
    {
        var certification = await Certifications.Where(x =>
                x.OrganizationId == organizationId && x.PersonaId == personId && x.Id == certificationId)
            .FirstOrDefaultAsync();

        if (certification == null)
            return Result.Failed();

        certification.ObjectState = ObjectState.Deleted;

        var records = Repository.InsertOrUpdateGraph(certification, true);
        if (records > 0) return Result.Success();

        return Result.Failed();
    }

    private async Task<int> GetNextCertificationId(int organizationId, int personId)
    {
        var certification = await Certifications.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.PersonaId == personId)
            .OrderByDescending(x => x.Id)
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync();

        if (certification == null) return 1;

        return certification.Id + 1;
    }
}