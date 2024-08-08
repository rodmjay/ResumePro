#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public sealed class IndividualProjectsProxy(HttpClient httpClient)
    : BaseProxy(httpClient), IIndividualProjectsController
{
    public async Task<ProjectDetails> GetProject(int jobId, int projectId)
    {
        return await DoGet<ProjectDetails>($"v1.0/jobs/{jobId}/projects/{projectId}")
            .ConfigureAwait(false);
    }

    public async Task<List<ProjectDetails>> GetList(int jobId)
    {
        return await DoGet<List<ProjectDetails>>($"v1.0/jobs/{jobId}/projects")
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<ProjectDetails>> CreateProject(int jobId, ProjectOptions options)
    {
        return await DoPostActionResult<ProjectOptions, ProjectDetails>($"v1.0/jobs/{jobId}/projects",
            options).ConfigureAwait(false);
    }

    public async Task<ActionResult<ProjectDetails>> Update(int jobId, int projectId,
        ProjectOptions options)
    {
        return await DoPutActionResult<ProjectOptions, ProjectDetails>(
                $"v1.0/jobs/{jobId}/projects/{projectId}", options)
            .ConfigureAwait(false);
    }

    public async Task<Result> Delete(int jobId, int projectId)
    {
        return await DoDelete<Result>($"v1.0/jobs/{jobId}/projects/{projectId}")
            .ConfigureAwait(false);
    }
}