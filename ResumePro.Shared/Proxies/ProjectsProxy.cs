#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public sealed class ProjectsProxy(HttpClient httpClient) : BaseProxy(httpClient), IProjectsController
{
    public async Task<ProjectDetails> GetProject(int personId, int companyId, int positionId, int projectId)
    {
        return await DoGet<ProjectDetails>($"v1.0/people/{personId}/companies/{companyId}/positions/{positionId}/projects/{projectId}")
            .ConfigureAwait(false);
    }

    public async Task<List<ProjectDetails>> GetList(int personId, int companyId, int positionId)
    {
        return await DoGet<List<ProjectDetails>>($"v1.0/people/{personId}/companies/{companyId}/positions/{positionId}/projects")
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<ProjectDetails>> CreateProject(int personId, int companyId, int positionId, ProjectOptions options)
    {
        return await DoPostActionResult<ProjectOptions, ProjectDetails>($"v1.0/people/{personId}/companies/{companyId}/positions/{positionId}/projects",
            options).ConfigureAwait(false);
    }

    public async Task<ActionResult<ProjectDetails>> Update(int personId, int companyId, int positionId, int projectId,
        ProjectOptions options)
    {
        return await DoPutActionResult<ProjectOptions, ProjectDetails>(
                $"v1.0/people/{personId}/companies/{companyId}/positions/{positionId}/projects/{projectId}", options)
            .ConfigureAwait(false);
    }

    public async Task<Result> Delete(int personId, int companyId, int positionId, int projectId)
    {
        return await DoDelete<Result>($"v1.0/people/{personId}/companies/{companyId}/positions/{positionId}/projects/{projectId}")
            .ConfigureAwait(false);
    }
}