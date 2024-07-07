#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using OneOf;
using ResumePro.Core.Services.Interfaces;
using ResumePro.Entities;
using ResumePro.Shared;
using ResumePro.Shared.Common;
using ResumePro.Shared.Options;

namespace ResumePro.Interfaces;

public interface IDegreeService : IService<Degree>
{
    Task<OneOf<DegreeDto, Result>> CreateDegree(int organizationId, int personId, int schoolId, DegreeOptions options);

    Task<Result> DeleteDegree(int organizationId, int personId, int schoolId, int degreeId);
}