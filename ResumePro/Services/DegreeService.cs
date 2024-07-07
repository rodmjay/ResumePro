#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using OneOf;
using ResumePro.Core.Services.Bases;
using ResumePro.Entities;
using ResumePro.Interfaces;
using ResumePro.Shared;
using ResumePro.Shared.Common;
using ResumePro.Shared.Options;

namespace ResumePro.Services;

public class DegreeService : BaseService<Degree>, IDegreeService
{
    public DegreeService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public Task<OneOf<DegreeDto, Result>> CreateDegree(int organizationId, int personId, int schoolId,
        DegreeOptions options)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteDegree(int organizationId, int personId, int schoolId, int degreeId)
    {
        throw new NotImplementedException();
    }
}