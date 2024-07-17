#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Interfaces;

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