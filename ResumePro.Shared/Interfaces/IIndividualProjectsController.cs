#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Interfaces;

public interface IIndividualProjectsController
{
    Task<ProjectDetails> GetProject(int jobId,
        int projectId);

    Task<List<ProjectDetails>> GetList(int jobId);

    Task<ActionResult<ProjectDetails>> CreateProject(int jobId,
        ProjectOptions options);

    Task<ActionResult<ProjectDetails>> Update(int jobId,
        int projectId, ProjectOptions options);

    Task<Result> Delete(int jobId,
        int projectId);
}