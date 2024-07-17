#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Services.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Interfaces;

public interface ISchoolService : IService<School>
{
    Task<List<T>> GetSchools<T>(int organizationId, int personId) where T : SchoolDto;
    Task<T> GetSchool<T>(int organizationId, int personId, int schoolId) where T : SchoolDto;
    Task<OneOf<SchoolDetails, Result>> CreateSchool(int organizationId, int personId, SchoolOptions options);

    Task<OneOf<SchoolDetails, Result>> UpdateSchool(int organizationId, int personId, int schoolId,
        SchoolOptions options);

    Task<Result> DeleteSchool(int organizationId, int personId, int schoolId);
}