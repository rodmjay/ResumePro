#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public class SchoolsProxy(HttpClient httpClient) : BaseProxy(httpClient), ISchoolsController
{
    public async Task<List<SchoolDetails>> GetSchools(int personId)
    {
        throw new NotImplementedException();
    }

    public async Task<SchoolDetails> GetSchool(int personId, int schoolId)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<SchoolDetails>> UpdateSchool(int personId, int schoolId, SchoolOptions options)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> DeleteSchool(int personId, int schoolId)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<SchoolDetails>> CreateSchool(int personId, SchoolOptions options)
    {
        throw new NotImplementedException();
    }
}