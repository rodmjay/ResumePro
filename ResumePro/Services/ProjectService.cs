﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OneOf;
using ResumePro.Core.Data.Enums;
using ResumePro.Core.Services.Bases;
using ResumePro.Entities;
using ResumePro.Interfaces;
using ResumePro.Shared;
using ResumePro.Shared.Common;
using ResumePro.Shared.Options;

namespace ResumePro.Services;

public class ProjectService : BaseService<Project>, IProjectService
{
    public ProjectService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    private IQueryable<Project> Projects => Repository.Queryable();

    public Task<List<T>> GetProjects<T>(int organizationId, int jobId) where T : ProjectDto
    {
        return Projects.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.JobId == jobId)
            .ProjectTo<T>(ProjectionMapping)
            .ToListAsync();
    }

    public Task<T> GetProject<T>(int organizationId, int projectId) where T : ProjectDto
    {
        return Projects.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.Id == projectId)
            .ProjectTo<T>(ProjectionMapping)
            .FirstOrDefaultAsync();
    }

    public async Task<OneOf<ProjectDetails, Result>> CreateProject(int organizationId, int jobId, ProjectOptions options)
    {
        var project = new Project
        {
            Id = await GetNextProjectId(organizationId),
            OrganizationId = organizationId,
            ObjectState = ObjectState.Added,
            Budget = options.Budget,
            Description = options.Description,
            JobId = jobId,
            Name = options.Name,
            Order = options.Order
        };

        var results = Repository.InsertOrUpdateGraph(project, true);
        if (results > 0)
        {
            return await GetProject<ProjectDetails>(organizationId, project.Id);
        }

        return Result.Failed();
    }

    public async Task<OneOf<ProjectDetails, Result>> UpdateProject(int organizationId, int jobId, int projectId, ProjectOptions options)
    {
        var project = await Projects.Where(x => x.OrganizationId == organizationId && x.Id == projectId)
            .FirstOrDefaultAsync();

        if (project == null)
            return Result.Failed();

        project.ObjectState = ObjectState.Modified;
        project.Budget = options.Budget;
        project.Description = options.Description;
        project.JobId = jobId;
        project.Name = options.Name;
        project.Order = options.Order;

        var results = Repository.InsertOrUpdateGraph(project, true);
        if (results > 0)
        {
            return await GetProject<ProjectDetails>(organizationId, project.Id);
        }

        return Result.Failed();
    }

    public async Task<Result> DeleteProject(int organizationId, int jobId, int projectId)
    {
        var project = await Projects
            .Include(x=>x.Highlights)
            .Where(x => x.OrganizationId == organizationId && x.Id == projectId)
            .FirstOrDefaultAsync();

        if (project == null)
            return Result.Failed();

        project.ObjectState = ObjectState.Deleted;

        foreach (var highlight in project.Highlights)
        {
            highlight.ObjectState = ObjectState.Deleted;
        }

        var changes = Repository.InsertOrUpdateGraph(project, true);
        if (changes > 0)
        {
            return Result.Success();
        }

        return Result.Failed();
    }

    private async Task<int> GetNextProjectId(int organizationId)
    {
        var project = await Projects.IgnoreQueryFilters()
            .Where(x => x.OrganizationId == organizationId)
            .AsNoTracking()
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync();

        if (project == null)
        {
            return 1;
        }

        return project.Id + 1;
    }
}