#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Interfaces;

public interface IIndividualDegreesController
{
    Task<DegreeDto> GetDegree(int schoolId, int degreeId);
    Task<List<DegreeDto>> GetDegrees(int schoolId);

    Task<ActionResult<DegreeDto>> UpdateDegree(int schoolId,
        int degreeId, DegreeOptions options);

    Task<Result> DeleteDegree(int schoolId,
        int degreeId);

    Task<ActionResult<DegreeDto>> CreateDegree(int schoolId, DegreeOptions options);
}