#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public class ProjectsProxy : BaseProxy, IProjectsController
{
    public ProjectsProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<ProjectDetails> GetProject(int personId, int jobId, int projectId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ProjectDetails>> GetList(int personId, int jobId)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<ProjectDetails>> Create(int personId, int jobId, ProjectOptions options)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<ProjectDetails>> Update(int personId, int jobId, int projectId, ProjectOptions options)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> Delete(int personId, int jobId, int projectId)
    {
        throw new NotImplementedException();
    }
}