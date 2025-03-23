using Bespoke.Services.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Services.Interfaces;

public interface IDegreeService : IService<Degree>
{
    Task<T> GetDegree<T>(int organizationId, int personId, int schoolId, int degreeId) where T : DegreeDto;
    Task<List<T>> GetDegrees<T>(int organizationId, int personId, int schoolId) where T : DegreeDto;

    Task<OneOf<DegreeDto, Result>> CreateDegree(int organizationId, int personId, int schoolId, DegreeOptions options);

    Task<OneOf<DegreeDto, Result>> UpdateDegree(int organizationId, int personId, int schoolId, int degreeId,
        DegreeOptions options);

    Task<Result> DeleteDegree(int organizationId, int personId, int schoolId, int degreeId);
}