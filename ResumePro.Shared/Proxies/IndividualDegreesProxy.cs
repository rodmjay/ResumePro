#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public sealed class IndividualDegreesProxy(HttpClient httpClient) : BaseProxy(httpClient), IIndividualDegreesController
{
    public async Task<DegreeDto> GetDegree(int schoolId, int degreeId)
    {
        return await DoGet<DegreeDto>($"v1.0/schools/{schoolId}/degrees/{degreeId}")
            .ConfigureAwait(false);
    }

    public async Task<List<DegreeDto>> GetDegrees(int schoolId)
    {
        return await DoGet<List<DegreeDto>>($"v1.0/schools/{schoolId}/degrees")
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<DegreeDto>> UpdateDegree(int schoolId, int degreeId,
        DegreeOptions options)
    {
        return await DoPutActionResult<DegreeOptions, DegreeDto>(
                $"v1.0/schools/{schoolId}/degrees/{degreeId}", options)
            .ConfigureAwait(false);
    }

    public async Task<Result> DeleteDegree(int schoolId, int degreeId)
    {
        return await DoDelete<Result>($"v1.0/schools/{schoolId}/degrees/{degreeId}")
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<DegreeDto>> CreateDegree(int schoolId, DegreeOptions options)
    {
        return await DoPostActionResult<DegreeOptions, DegreeDto>($"v1.0/schools/{schoolId}/degrees",
                options)
            .ConfigureAwait(false);
    }
}