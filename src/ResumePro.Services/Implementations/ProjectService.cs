using System.Diagnostics.CodeAnalysis;
using Bespoke.Data.Enums;
using Bespoke.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResumePro.Services.ErrorDescribers;
using ResumePro.Services.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Services.Implementations;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class ProjectService : BaseService<Project>, IProjectService
{
    private readonly ProjectErrorDescriber _projectErrors;

    public ProjectService(IServiceProvider serviceProvider, ProjectErrorDescriber projectErrors) : base(serviceProvider)
    {
        _projectErrors = projectErrors;
    }

    private IQueryable<Project> Projects => Repository.Queryable();

    public Task<List<T>> GetProjects<T>(int organizationId, int personId, int companyId, int positionId)
        where T : ProjectDto
    {
        return Projects.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId
                        && x.PersonId == personId
                        && x.CompanyId == companyId
                        && x.PositionId == positionId)
            .OrderBy(x => x.Order)
            .ProjectTo<T>(Mapper)
            .ToListAsync();
    }

    public Task<T> GetProject<T>(int organizationId, int personId, int companyId, int positionId, int projectId)
        where T : ProjectDto
    {
        return Projects.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId
                        && x.PersonId == personId
                        && x.CompanyId == companyId
                        && x.PositionId == positionId
                        && x.Id == projectId)
            .ProjectTo<T>(Mapper)
            .FirstAsync();
    }

    public async Task<OneOf<ProjectDetails, Result>> CreateProject(int organizationId, int companyId, int personId,
        int positionId,
        ProjectOptions options)
    {
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, CompanyId: {@companyId}, PositionId, {@positionId}, Options: {@options}"),
            organizationId, personId, companyId, positionId, options);

        var lastProjectOrder = await
            Projects.Where(x => x.OrganizationId == organizationId
                                && x.PersonId == personId
                                && x.CompanyId == companyId
                                && x.PositionId == positionId)
                .AsNoTracking()
                .OrderByDescending(x => x.Order)
                .Select(x => x.Order)
                .FirstOrDefaultAsync();

        var project = new Project
        {
            Id = await GetNextProjectId(organizationId, personId, companyId, positionId),
            OrganizationId = organizationId,
            ObjectState = ObjectState.Added,
            Budget = options.Budget,
            PersonId = personId,
            PositionId = positionId,
            Description = options.Description,
            CompanyId = companyId,
            Name = options.Name,
            Order = lastProjectOrder + 1
        };

        var results = Repository.InsertOrUpdateGraph(project, true);
        if (results > 0)
            return await GetProject<ProjectDetails>(organizationId, personId, companyId, positionId, project.Id);

        return Result.Failed(_projectErrors.UnableToSaveProject());
    }

    public async Task<OneOf<ProjectDetails, Result>> UpdateProject(int organizationId, int personId, int companyId,
        int positionId, int projectId,
        ProjectOptions options)
    {
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, CompanyId: {@companyId}, PositionId: {@positionId}, ProjectId: {@projectId}, Options: {@options}"),
            organizationId, personId, companyId, positionId, projectId, options);

        var projects = await Projects
            .Where(x => x.OrganizationId == organizationId
                        && x.PersonId == personId
                        && x.CompanyId == companyId
                        && x.PositionId == positionId)
            .OrderBy(x => x.Order)
            .ToListAsync();

        var project = projects.FirstOrDefault(x => x.Id == projectId);

        if (project == null)
            return Result.Failed(_projectErrors.ProjectNotFound(projectId));

        projects.Remove(project);

        project.ObjectState = ObjectState.Modified;
        project.Budget = options.Budget;
        project.Description = options.Description;
        project.CompanyId = companyId;
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
        if (results > 0)
            return await GetProject<ProjectDetails>(organizationId, personId, companyId, positionId, project.Id);

        return Result.Failed(_projectErrors.UnableToSaveProject());
    }

    public async Task<Result> DeleteProject(int organizationId, int personId, int companyId, int positionId,
        int projectId)
    {
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, CompanyId: {@companyId}, PositionId: {@positionId}, ProjectId: {@projectId}"),
            organizationId, personId, companyId, positionId, projectId);

        var project = await Projects
            .Include(x => x.Highlights)
            .Where(x => x.OrganizationId == organizationId
                        && x.PersonId == personId
                        && x.CompanyId == companyId
                        && x.PositionId == positionId
                        && x.Id == projectId)
            .FirstOrDefaultAsync();

        if (project == null)
            return Result.Failed(_projectErrors.ProjectNotFound(projectId));

        var projects = await Projects
            .Where(x => x.OrganizationId == organizationId && x.CompanyId == companyId)
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

        return Result.Failed(_projectErrors.UnableToSaveProject());
    }

    private async Task<int> GetNextProjectId(int organizationId, int personId, int companyId, int positionId)
    {
        var id = await Projects.AsNoTracking()
            .IgnoreQueryFilters()
            .Where(x => x.OrganizationId == organizationId && x.PersonId == personId && x.CompanyId == companyId &&
                        x.PositionId == positionId)
            .OrderByDescending(x => x.Id)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
        return id + 1;
    }
}