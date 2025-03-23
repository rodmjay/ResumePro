namespace ResumePro.Api.Interfaces;

public interface ISchoolsController
{
    Task<List<SchoolDetails>> GetSchools(int personId);
    Task<SchoolDetails> GetSchool(int personId, int schoolId);

    Task<ActionResult<SchoolDetails>> UpdateSchool(int personId,
        int schoolId,
        SchoolOptions options);

    Task<Result> DeleteSchool(int personId,
        int schoolId);

    Task<ActionResult<SchoolDetails>> CreateSchool(int personId,
        SchoolOptions options);
}