#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class DegreeService : BaseService<Degree>, IDegreeService
{
    private readonly DegreeErrorDescriber _errors;

    public DegreeService(IServiceProvider serviceProvider, DegreeErrorDescriber errors) : base(serviceProvider)
    {
        _errors = errors;
    }

    private IQueryable<Degree> Degrees => Repository.Queryable();

    public Task<T> GetDegree<T>(int organizationId, int personId, int schoolId, int degreeId) where T : DegreeDto
    {
        return Degrees.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.SchoolId == schoolId && x.Id == degreeId)
            .ProjectTo<T>(Mapper)
            .FirstOrDefaultAsync();
    }

    public Task<List<T>> GetDegrees<T>(int organizationId, int personId, int schoolId) where T : DegreeDto
    {
        return Degrees.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.SchoolId == schoolId)
            .ProjectTo<T>(Mapper)
            .ToListAsync();
    }

    public async Task<OneOf<DegreeDto, Result>> CreateDegree(int organizationId, int personId, int schoolId,
        DegreeOptions options)
    {
        var degree = new Degree()
        {
            ObjectState = ObjectState.Added,
            OrganizationId = organizationId,
            SchoolId = schoolId,
            Name = options.Name,
            Id = await GetNextDegreeId(organizationId)
        };

        var changes = Repository.InsertOrUpdateGraph(degree, true);
        if (changes > 0)
        {
            return await GetDegree<DegreeDto>(organizationId, personId, schoolId, degree.Id);
        }

        return Result.Failed(_errors.UnableToSaveDegree());
    }

    public async Task<OneOf<DegreeDto, Result>> UpdateDegree(int organizationId, int personId, int schoolId, int degreeId, DegreeOptions options)
    {
        var degree = await Degrees
            .Where(x => x.OrganizationId == organizationId && x.SchoolId == schoolId && x.Id == degreeId)
            .FirstOrDefaultAsync();

        if (degree == null)
            return Result.Failed(_errors.DegreeNotFound(degreeId));

        degree.ObjectState = ObjectState.Modified;
        degree.Name = options.Name;

        var changes = Repository.InsertOrUpdateGraph(degree, true);
        if (changes > 0)
        {
            return await GetDegree<DegreeDto>(organizationId, personId, schoolId, degree.Id);
        }

        return Result.Failed(_errors.UnableToSaveDegree());
    }

    public async Task<Result> DeleteDegree(int organizationId, int personId, int schoolId, int degreeId)
    {
        var degree = await Degrees
            .Where(x => x.OrganizationId == organizationId && x.SchoolId == schoolId && x.Id == degreeId)
            .FirstOrDefaultAsync();

        if (degree == null)
            return Result.Failed(_errors.DegreeNotFound(degreeId));

        degree.ObjectState = ObjectState.Deleted;

        var changes = Repository.InsertOrUpdateGraph(degree, true);
        return changes > 0 ? Result.Success() : Result.Failed();
    }

    private async Task<int> GetNextDegreeId(int organizationId)
    {
        var degree = await Degrees.Where(x => x.OrganizationId == organizationId)
            .AsNoTracking()
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync();

        if (degree == null)
            return 1;

        return degree.Id + 1;
    }
}