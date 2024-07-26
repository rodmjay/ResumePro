#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public sealed class DegreesProxy(HttpClient httpClient) : BaseProxy(httpClient), IDegreesController
{
    public async Task<DegreeDto> GetDegree(int personId, int schoolId, int degreeId)
    {
        return await DoGet<DegreeDto>($"v1.0/people/{personId}/schools/{schoolId}/degrees/{degreeId}")
            .ConfigureAwait(false);
    }

    public async Task<List<DegreeDto>> GetDegrees(int personId, int schoolId)
    {
        return await DoGet<List<DegreeDto>>($"v1.0/people/{personId}/schools/{schoolId}/degrees")
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<DegreeDto>> UpdateDegree(int personId, int schoolId, int degreeId,
        DegreeOptions options)
    {
        return await DoPutActionResult<DegreeOptions, DegreeDto>(
                $"v1.0/people/{personId}/schools/{schoolId}/degrees/{degreeId}", options)
            .ConfigureAwait(false);
    }

    public async Task<Result> DeleteDegree(int personId, int schoolId, int degreeId)
    {
        return await DoDelete<Result>($"v1.0/people/{personId}/schools/{schoolId}/degrees/{degreeId}")
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<DegreeDto>> CreateDegree(int personId, int schoolId, DegreeOptions options)
    {
        return await DoPostActionResult<DegreeOptions, DegreeDto>($"v1.0/people/{personId}/schools/{schoolId}/degrees",
                options)
            .ConfigureAwait(false);
    }
}