using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Interfaces;

public interface IIndividualSchoolsController
{
    Task<List<SchoolDetails>> GetSchools();
    Task<SchoolDetails> GetSchool(int schoolId);

    Task<ActionResult<SchoolDetails>> UpdateSchool(
        int schoolId,
        SchoolOptions options);

    Task<Result> DeleteSchool(
        int schoolId);

    Task<ActionResult<SchoolDetails>> CreateSchool(
        SchoolOptions options);
}