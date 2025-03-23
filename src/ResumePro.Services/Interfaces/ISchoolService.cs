using Bespoke.Services.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Services.Interfaces;

public interface ISchoolService : IService<School>
{
    Task<List<T>> GetSchools<T>(int organizationId, int personId) where T : SchoolDto;
    Task<T> GetSchool<T>(int organizationId, int personId, int schoolId) where T : SchoolDto;
    Task<OneOf<SchoolDetails, Result>> CreateSchool(int organizationId, int personId, SchoolOptions options);

    Task<OneOf<SchoolDetails, Result>> UpdateSchool(int organizationId, int personId, int schoolId,
        SchoolOptions options);

    Task<Result> DeleteSchool(int organizationId, int personId, int schoolId);
}