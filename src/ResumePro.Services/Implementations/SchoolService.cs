using System.Diagnostics.CodeAnalysis;
using Bespoke.Data.Enums;
using Bespoke.Data.Extensions;
using Bespoke.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResumePro.Services.ErrorDescribers;
using ResumePro.Services.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Services.Implementations;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class SchoolService : BaseService<School>, ISchoolService
{
    private readonly IRepositoryAsync<Degree> _degreeRepo;
    private readonly SchoolErrorDescriber _schoolErrors;

    public SchoolService(IServiceProvider serviceProvider,
        IRepositoryAsync<Degree> degreeRepo,
        SchoolErrorDescriber schoolErrors) : base(serviceProvider)
    {
        _degreeRepo = degreeRepo;
        _schoolErrors = schoolErrors;
    }

    private IQueryable<School> Schools => Repository.Queryable();
    private IQueryable<Degree> Degrees => _degreeRepo.Queryable();

    public Task<List<T>> GetSchools<T>(int organizationId, int personId) where T : SchoolDto
    {
        return Schools.Where(x => x.OrganizationId == organizationId && x.PersonId == personId)
            .AsNoTracking()
            .ProjectTo<T>(Mapper)
            .ToListAsync();
    }

    public Task<T> GetSchool<T>(int organizationId, int personId, int schoolId) where T : SchoolDto
    {
        return Schools.Where(x => x.OrganizationId == organizationId && x.PersonId == personId && x.Id == schoolId)
            .AsNoTracking()
            .ProjectTo<T>(Mapper)
            .FirstAsync();
    }

    public async Task<OneOf<SchoolDetails, Result>> CreateSchool(int organizationId, int personId,
        SchoolOptions options)
    {
        Logger.LogInformation(
            GetLogMessage("OrganizationId: {@organizationId}, PersonId: {@personId}, Options: {@options}"),
            organizationId, personId, options);

        var school = new School
        {
            Id = await GetNextSchoolId(organizationId),
            ObjectState = ObjectState.Added,
            EndDate = options.EndDate,
            StartDate = options.StartDate!.Value,
            Name = options.Name,
            Location = options.Location,
            PersonId = personId,
            OrganizationId = organizationId
        };

        var nextDegreeId = await GetNextDegreeId(organizationId);

        for (var i = 0; i < options.DegreeOptions.Count; i++)
        {
            var degreeOptions = options.DegreeOptions[i];
            var degree = new Degree
            {
                Id = nextDegreeId++,
                ObjectState = ObjectState.Added,
                Name = degreeOptions.Name,
                OrganizationId = organizationId,
                Order = i + 1
            };

            school.Degrees.Add(degree);
        }

        try
        {
            var results = Repository.InsertOrUpdateGraph(school, true);
            if (results > 0) return await GetSchool<SchoolDetails>(organizationId, personId, school.Id);

            return Result.Failed(_schoolErrors.UnableToSaveSchool());

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
       
    }

    public async Task<OneOf<SchoolDetails, Result>> UpdateSchool(int organizationId, int personId, int schoolId,
        SchoolOptions options)
    {
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, SchoolId: {@schoolId}, Options: {@options}"),
            organizationId, personId, schoolId, options);

        var school = await Schools
            .Include(x => x.Degrees)
            .Where(x => x.OrganizationId == organizationId && x.Id == schoolId)
            .FirstOrDefaultAsync();

        if (school == null)
            return Result.Failed(_schoolErrors.SchoolNotFound(schoolId));

        school.Name = options.Name;
        school.EndDate = options.EndDate;
        school.StartDate = options.StartDate!.Value;
        school.ObjectState = ObjectState.Modified;

        var nextDegreeId = await GetNextDegreeId(organizationId);

        foreach (var degree in school.Degrees) degree.ObjectState = ObjectState.Deleted;

        for (var i = 0; i < options.DegreeOptions.Count; i++)
        {
            var degreeOptions = options.DegreeOptions[i];
            var degreeEntity = school.Degrees.FirstOrDefault(x => x.Id == degreeOptions.Id);
            if (degreeEntity == null)
            {
                degreeEntity = new Degree
                {
                    ObjectState = ObjectState.Added,
                    Id = nextDegreeId++
                };
                school.Degrees.Add(degreeEntity);
            }
            else
            {
                degreeEntity.ObjectState = ObjectState.Modified;
            }

            degreeEntity.Order = i + 1;
            degreeEntity.Name = degreeOptions.Name;
        }

        var result = Repository.InsertOrUpdateGraph(school, true);
        if (result > 0) return await GetSchool<SchoolDetails>(organizationId, personId, schoolId);

        return Result.Failed(_schoolErrors.SchoolNotFound(schoolId));
    }

    public async Task<Result> DeleteSchool(int organizationId, int personId, int schoolId)
    {
        Logger.LogInformation(
            GetLogMessage("OrganizationId: {@organizationId}, PersonId: {@personId}, SchoolId: {@schoolId}"),
            organizationId, personId, schoolId);

        var school = await Schools.Include(x => x.Degrees)
            .Where(x => x.Id == schoolId && x.OrganizationId == organizationId)
            .FirstOrDefaultAsync();

        if (school == null)
            return Result.Failed(_schoolErrors.SchoolNotFound(schoolId));

        school.ObjectState = ObjectState.Deleted;

        var results = Repository.InsertOrUpdateGraph(school, true);
        if (results > 0) return Result.Success();

        return Result.Failed();
    }

    private async Task<int> GetNextSchoolId(int organizationId)
    {
        var id = await Schools.AsNoTracking()
            .IgnoreQueryFilters()
            .Where(x => x.OrganizationId == organizationId)
            .OrderByDescending(x => x.Id)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
        return id + 1;
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