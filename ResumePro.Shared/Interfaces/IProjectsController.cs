#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Interfaces;

public interface IProjectsController
{
    Task<ProjectDetails> GetProject(int personId, int jobId,
        int projectId);

    Task<List<ProjectDetails>> GetList(int personId, int jobId);

    Task<ActionResult<ProjectDetails>> CreateProject(int personId, int jobId,
        ProjectOptions options);

    Task<ActionResult<ProjectDetails>> Update(int personId, int jobId,
        int projectId, ProjectOptions options);

    Task<Result> Delete(int personId, int jobId,
        int projectId);
}