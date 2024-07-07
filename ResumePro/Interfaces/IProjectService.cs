#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using OneOf;
using ResumePro.Core.Services.Interfaces;
using ResumePro.Entities;
using ResumePro.Shared;
using ResumePro.Shared.Common;
using ResumePro.Shared.Options;

namespace ResumePro.Interfaces;

public interface IProjectService : IService<Project>
{
    Task<List<T>> GetProjects<T>(int organizationId, int jobId) where T : ProjectDto;
    Task<T> GetProject<T>(int organizationId, int projectId) where T : ProjectDto;
    Task<OneOf<ProjectDetails, Result>> CreateProject(int organizationId, int jobId, ProjectOptions options);

    Task<OneOf<ProjectDetails, Result>> UpdateProject(int organizationId, int jobId, int projectId,
        ProjectOptions options);

    Task<Result> DeleteProject(int organizationId, int jobId, int projectId);
}