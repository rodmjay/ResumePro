using System.Diagnostics.CodeAnalysis;
using Bespoke.Data.Enums;
using Bespoke.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResumePro.Services.ErrorDescribers;
using ResumePro.Services.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Services.Implementations;

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
            .FirstAsync();
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
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, SchoolId {@schoolId}, Options: {@options}"),
            organizationId, personId, schoolId, options);

        var lastOrder = await
            Degrees.Where(x => x.OrganizationId == organizationId && x.SchoolId == schoolId)
                .AsNoTracking()
                .OrderByDescending(x => x.Order)
                .Select(x => x.Order)
                .FirstOrDefaultAsync();

        var degree = new Degree
        {
            ObjectState = ObjectState.Added,
            OrganizationId = organizationId,
            SchoolId = schoolId,
            Name = options.Name,
            Id = await GetNextDegreeId(organizationId),
            Order = lastOrder
        };

        var changes = Repository.InsertOrUpdateGraph(degree, true);
        if (changes > 0) return await GetDegree<DegreeDto>(organizationId, personId, schoolId, degree.Id);

        return Result.Failed(_errors.UnableToSaveDegree());
    }

    public async Task<OneOf<DegreeDto, Result>> UpdateDegree(int organizationId, int personId, int schoolId,
        int degreeId, DegreeOptions options)
    {
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, SchoolId {@schoolId}, Degree: {@degreeId}, Options: {@options}"),
            organizationId, personId, schoolId, degreeId, options);

        var degrees = await Degrees
            .Where(x => x.OrganizationId == organizationId && x.SchoolId == schoolId)
            .OrderBy(x => x.Order)
            .ToListAsync();

        var degree = degrees.FirstOrDefault(x => x.Id == degreeId);

        if (degree == null)
            return Result.Failed(_errors.DegreeNotFound(degreeId));

        degrees.Remove(degree);

        degree.ObjectState = ObjectState.Modified;
        degree.Name = options.Name;
        degree.Order = options.Order;

        var index = options.Order - 1;
        if (index < 0) index = 0;
        if (index > degrees.Count) index = degrees.Count;

        degrees.Insert(index, degree);

        for (var i = 0; i < degrees.Count; i++)
        {
            degrees[i].Order = i + 1;
            degrees[i].ObjectState = ObjectState.Modified;

            Repository.InsertOrUpdateGraph(degrees[0]);
        }

        var changes = Repository.Commit();
        if (changes > 0) return await GetDegree<DegreeDto>(organizationId, personId, schoolId, degree.Id);

        return Result.Failed(_errors.UnableToSaveDegree());
    }

    public async Task<Result> DeleteDegree(int organizationId, int personId, int schoolId, int degreeId)
    {
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, SchoolId {@schoolId}, Degree: {@degreeId}"),
            organizationId, personId, schoolId, degreeId);

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
        var id = await Degrees.AsNoTracking()
            .IgnoreQueryFilters()
            .Where(x => x.OrganizationId == organizationId)
            .OrderByDescending(x => x.Id)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
        return id + 1;
    }
}