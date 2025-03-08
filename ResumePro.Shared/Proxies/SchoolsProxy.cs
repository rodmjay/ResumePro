#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;

namespace ResumePro.Shared.Proxies;

public sealed class SchoolsProxy : BaseProxy, ISchoolsController
{
    public SchoolsProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<List<SchoolDetails>> GetSchools(int personId)
    {
        return await DoGet<List<SchoolDetails>>($"v1.0/people/{personId}/schools")
            .ConfigureAwait(false);
    }

    public async Task<SchoolDetails> GetSchool(int personId, int schoolId)
    {
        return await DoGet<SchoolDetails>($"v1.0/people/{personId}/schools/{schoolId}")
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<SchoolDetails>> UpdateSchool(int personId, int schoolId, SchoolOptions options)
    {
        return await DoPutActionResult<SchoolOptions, SchoolDetails>($"v1.0/people/{personId}/schools/{schoolId}",
            options).ConfigureAwait(false);
    }

    public async Task<Result> DeleteSchool(int personId, int schoolId)
    {
        return await DoDelete<Result>($"v1.0/people/{personId}/schools/{schoolId}")
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<SchoolDetails>> CreateSchool(int personId, SchoolOptions options)
    {
        return await DoPostActionResult<SchoolOptions, SchoolDetails>($"v1.0/people/{personId}/schools", options)
            .ConfigureAwait(false);
    }
}