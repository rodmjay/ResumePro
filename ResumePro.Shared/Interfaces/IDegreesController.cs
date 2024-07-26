#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Interfaces;

public interface IDegreesController
{
    Task<DegreeDto> GetDegree(int personId, int schoolId, int degreeId);
    Task<List<DegreeDto>> GetDegrees(int personId, int schoolId);

    Task<ActionResult<DegreeDto>> UpdateDegree(int personId, int schoolId,
        int degreeId, DegreeOptions options);

    Task<Result> DeleteDegree(int personId, int schoolId,
        int degreeId);

    Task<ActionResult<DegreeDto>> CreateDegree(int personId, int schoolId, DegreeOptions options);
}