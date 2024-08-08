#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public sealed class IndividualSchoolsProxy(HttpClient client) : BaseProxy(client), IIndividualSchoolsController
{
    public async Task<List<SchoolDetails>> GetSchools()
    {
        return await DoGet<List<SchoolDetails>>($"v1.0/schools")
            .ConfigureAwait(false);
    }

    public async Task<SchoolDetails> GetSchool(int schoolId)
    {
        return await DoGet<SchoolDetails>($"v1.0/schools/{schoolId}")
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<SchoolDetails>> UpdateSchool(int schoolId, SchoolOptions options)
    {
        return await DoPutActionResult<SchoolOptions, SchoolDetails>($"v1.0/schools/{schoolId}",
            options).ConfigureAwait(false);
    }

    public async Task<Result> DeleteSchool(int schoolId)
    {
        return await DoDelete<Result>($"v1.0/schools/{schoolId}")
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<SchoolDetails>> CreateSchool(SchoolOptions options)
    {
        return await DoPostActionResult<SchoolOptions, SchoolDetails>($"v1.0/schools", options)
            .ConfigureAwait(false);
    }
}