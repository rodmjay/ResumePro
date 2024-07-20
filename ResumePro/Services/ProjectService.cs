#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.Extensions.Logging;
using ResumePro.Shared.Models;

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class ProjectService(IServiceProvider serviceProvider, ProjectErrorDescriber projectErrors)
    : BaseService<Project>(serviceProvider), IProjectService
{
    private IQueryable<Project> Projects => Repository.Queryable();

    public Task<List<T>> GetProjects<T>(int organizationId, int jobId) where T : ProjectDto
    {
        return Projects.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.JobId == jobId)
            .OrderBy(x => x.Order)
            .ProjectTo<T>(Mapper)
            .ToListAsync();
    }

    public Task<T> GetProject<T>(int organizationId, int projectId) where T : ProjectDto
    {
        return Projects.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.Id == projectId)
            .ProjectTo<T>(Mapper)
            .FirstOrDefaultAsync();
    }

    public async Task<OneOf<ProjectDetails, Result>> CreateProject(int organizationId, int jobId,
        ProjectOptions options)
    {
        Logger.LogInformation(GetLogMessage("OrganizationId: {@organizationId}, JobId: {@jobId}, Options: {@options}"),
            organizationId, jobId, options);

        var lastProjectOrder = await
            Projects.Where(x => x.OrganizationId == organizationId && x.JobId == jobId)
                .AsNoTracking()
                .OrderByDescending(x => x.Order)
                .Select(x => x.Order)
                .FirstOrDefaultAsync();

        var project = new Project
        {
            Id = await GetNextProjectId(organizationId),
            OrganizationId = organizationId,
            ObjectState = ObjectState.Added,
            Budget = options.Budget,
            Description = options.Description,
            JobId = jobId,
            Name = options.Name,
            Order = lastProjectOrder + 1
        };

        var results = Repository.InsertOrUpdateGraph(project, true);
        if (results > 0) return await GetProject<ProjectDetails>(organizationId, project.Id);

        return Result.Failed(projectErrors.UnableToSaveProject());
    }

    public async Task<OneOf<ProjectDetails, Result>> UpdateProject(int organizationId, int jobId, int projectId,
        ProjectOptions options)
    {
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, JobId: {@jobId}, ProjectId: {@projectId}, Options: {@options}"),
            organizationId, jobId, projectId, options);

        var projects = await Projects
            .Where(x => x.OrganizationId == organizationId && x.JobId == jobId)
            .OrderBy(x => x.Order)
            .ToListAsync();

        var project = projects.FirstOrDefault(x => x.Id == projectId);

        if (project == null)
            return Result.Failed(projectErrors.ProjectNotFound(projectId));

        projects.Remove(project);

        project.ObjectState = ObjectState.Modified;
        project.Budget = options.Budget;
        project.Description = options.Description;
        project.JobId = jobId;
        project.Name = options.Name;
        project.Order = options.Order;

        var index = options.Order - 1;

        if (index < 0) index = 0;
        if (index > projects.Count) index = projects.Count;

        projects.Insert(index, project);

        for (var i = 0; i < projects.Count; i++)
        {
            projects[i].Order = i + 1;
            projects[i].ObjectState = ObjectState.Modified;

            Repository.InsertOrUpdateGraph(projects[0]);
        }

        var results = Repository.Commit();
        if (results > 0) return await GetProject<ProjectDetails>(organizationId, project.Id);

        return Result.Failed(projectErrors.UnableToSaveProject());
    }

    public async Task<Result> DeleteProject(int organizationId, int jobId, int projectId)
    {
        Logger.LogInformation(
            GetLogMessage("OrganizationId: {@organizationId}, JobId: {@jobId}, ProjectId: {@projectId}"),
            organizationId, jobId, projectId);

        var project = await Projects
            .Include(x => x.Highlights)
            .Where(x => x.OrganizationId == organizationId && x.Id == projectId)
            .FirstOrDefaultAsync();

        if (project == null)
            return Result.Failed(projectErrors.ProjectNotFound(projectId));

        var projects = await Projects
            .Where(x => x.OrganizationId == organizationId && x.JobId == jobId)
            .OrderBy(x => x.Order).ToListAsync();

        projects.Remove(project);

        for (var i = 0; i < projects.Count; i++)
        {
            projects[i].Order = i + 1;
            projects[i].ObjectState = ObjectState.Modified;

            Repository.InsertOrUpdateGraph(projects[i]);
        }

        project.ObjectState = ObjectState.Deleted;

        foreach (var highlight in project.Highlights)
            highlight.ObjectState = ObjectState.Deleted;

        Repository.InsertOrUpdateGraph(project);

        var changes = Repository.Commit();
        if (changes > 0) return Result.Success();

        return Result.Failed(projectErrors.UnableToSaveProject());
    }

    private async Task<int> GetNextProjectId(int organizationId)
    {
        var id = await Projects.AsNoTracking()
            .IgnoreQueryFilters()
            .Where(x => x.OrganizationId == organizationId)
            .OrderByDescending(x => x.Id)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
        return id + 1;
    }
}