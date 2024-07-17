#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public class DegreesProxy : BaseProxy, IDegreesController
{
    public DegreesProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<DegreeDto> GetDegree(int personId, int schoolId, int degreeId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<DegreeDto>> GetDegrees(int personId, int schoolId)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<DegreeDto>> UpdateDegree(int personId, int schoolId, int degreeId, DegreeOptions options)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> DeleteDegree(int personId, int schoolId, int degreeId)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<DegreeDto>> CreateDegree(int personId, int schoolId, DegreeOptions options)
    {
        throw new NotImplementedException();
    }
}