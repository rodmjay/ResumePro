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

public class SchoolService : BaseService<School>, ISchoolService
{
    public SchoolService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    private IQueryable<School> Schools => Repository.Queryable();

    public Task<List<T>> GetSchools<T>(int organizationId, int personId) where T : SchoolDto
    {
        return Schools.Where(x => x.OrganizationId == organizationId && x.PersonaId == personId)
            .AsNoTracking()
            .ProjectTo<T>(Mapper)
            .ToListAsync();
    }

    public Task<T> GetSchool<T>(int organizationId, int personId, int schoolId) where T : SchoolDto
    {
        return Schools.Where(x => x.OrganizationId == organizationId && x.PersonaId == personId && x.Id == schoolId)
            .AsNoTracking()
            .ProjectTo<T>(Mapper)
            .FirstOrDefaultAsync();
    }

    public async Task<OneOf<SchoolDetails, Result>> CreateSchool(int organizationId, int personId,
        SchoolOptions options)
    {
        var school = new School
        {
            Id = await GetNextSchoolId(organizationId),
            ObjectState = ObjectState.Added,
            EndDate = options.EndDate,
            StartDate = options.StartDate,
            Name = options.Name,
            PersonaId = personId,
            OrganizationId = organizationId
        };

        var results = Repository.InsertOrUpdateGraph(school, true);
        if (results > 0) return await GetSchool<SchoolDetails>(organizationId, personId, school.Id);

        return Result.Failed();
    }

    public async Task<OneOf<SchoolDetails, Result>> UpdateSchool(int organizationId, int personId, int schoolId,
        SchoolOptions options)
    {
        var school = await Schools
            .Where(x => x.OrganizationId == organizationId && x.Id == schoolId)
            .FirstOrDefaultAsync();

        if (school == null)
            return Result.Failed();

        school.Name = options.Name;
        school.EndDate = options.EndDate;
        school.StartDate = options.StartDate;
        school.ObjectState = ObjectState.Modified;

        var result = Repository.InsertOrUpdateGraph(school, true);
        if (result > 0) return await GetSchool<SchoolDetails>(organizationId, personId, schoolId);

        return Result.Failed();
    }

    public async Task<Result> DeleteSchool(int organizationId, int personId, int schoolId)
    {
        var school = await Schools.Include(x => x.Degrees)
            .Where(x => x.Id == schoolId && x.OrganizationId == organizationId)
            .FirstOrDefaultAsync();

        if (school == null)
            return Result.Failed();

        school.ObjectState = ObjectState.Deleted;

        var results = Repository.InsertOrUpdateGraph(school, true);
        if (results > 0) return Result.Success();

        return Result.Failed();
    }

    private async Task<int> GetNextSchoolId(int organizationId)
    {
        var school = await Schools.AsNoTracking()
            .IgnoreQueryFilters()
            .Where(x => x.OrganizationId == organizationId)
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync();

        if (school == null) return 1;

        return school.Id + 1;
    }
}