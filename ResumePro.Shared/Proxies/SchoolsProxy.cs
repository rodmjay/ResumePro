#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public class SchoolsProxy(HttpClient httpClient) : BaseProxy(httpClient), ISchoolsController
{
    public async Task<List<SchoolDetails>> GetSchools(int personId)
    {
        return await DoGet<List<SchoolDetails>>($"v1.0/people/{personId}/schools");
    }

    public async Task<SchoolDetails> GetSchool(int personId, int schoolId)
    {
        return await DoGet<SchoolDetails>($"v1.0/people/{personId}/schools/{schoolId}");
    }

    public async Task<ActionResult<SchoolDetails>> UpdateSchool(int personId, int schoolId, SchoolOptions options)
    {
        return await DoPutActionResult<SchoolOptions, SchoolDetails>($"v1.0/people/{personId}/schools/{schoolId}",
            options);
    }

    public async Task<Result> DeleteSchool(int personId, int schoolId)
    {
        return await DoDelete<Result>($"v1.0/people/{personId}/schools/{schoolId}");
    }

    public async Task<ActionResult<SchoolDetails>> CreateSchool(int personId, SchoolOptions options)
    {
        return await DoPostActionResult<SchoolOptions, SchoolDetails>($"v1.0/people/{personId}/schools", options);
    }
}