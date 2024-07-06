#region Header Info

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

public class JobService : BaseService<Job>, IJobService
{
    public JobService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    private IQueryable<Job> Jobs => Repository.Queryable();

    public Task<List<T>> GetJobs<T>(int organizationId, int personId) where T : JobDto
    {
        return Jobs.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.PersonaId == personId)
            .ProjectTo<T>(Mapper).ToListAsync();
    }

    public Task<T> GetJob<T>(int organizationId, int personaId, int jobId) where T : JobDto
    {
        return Jobs.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.PersonaId == personaId && x.Id == jobId)
            .ProjectTo<T>(Mapper).FirstOrDefaultAsync();
    }

    public async Task<OneOf<JobDetails, Result>> CreateJob(int organizationId, int personId, JobOptions options)
    {
        var job = new Job
        {
            Id = await GetNextJobId(organizationId),
            ObjectState = ObjectState.Added,
            StartDate = options.StartDate,
            Title = options.Title,
            EndDate = options.EndDate,
            Company = options.Company,
            Description = options.Description,
            Location = options.Location,
            OrganizationId = organizationId,
            PersonaId = personId
        };

        var results = Repository.InsertOrUpdateGraph(job, true);
        if (results > 0)
        {
            return await GetJob<JobDetails>(organizationId, personId, job.Id);
        }

        return Result.Failed();
    }

    public async Task<OneOf<JobDetails, Result>> UpdateJob(int organizationId, int personId, int jobId, JobOptions options)
    {
        var job = await Jobs.Where(x => x.OrganizationId == organizationId && x.PersonaId == personId && x.Id == jobId)
            .FirstOrDefaultAsync();

        if (job == null)
            return Result.Failed();

        job.ObjectState = ObjectState.Modified;
        job.Company = options.Company;
        job.Description = options.Description;
        job.EndDate = options.EndDate;
        job.StartDate = options.StartDate;
        job.Location = options.Location;
        job.Title = options.Title;

        var results = Repository.InsertOrUpdateGraph(job, true);
        if (results > 0)
        {
            return await GetJob<JobDetails>(organizationId, personId, jobId);
        }

        return Result.Failed();
    }

    public async Task<Result> DeleteJob(int organizationId, int personId, int jobId)
    {
        var job = await Jobs
            .Include(x=>x.Highlights)
            .Include(x=>x.Resumes)
            .Include(x=>x.Projects)
            .ThenInclude(x=>x.Highlights)
            .Include(x=>x.Skills)
            .Where(x => x.OrganizationId == organizationId && x.PersonaId == personId && x.Id == jobId)
            .FirstOrDefaultAsync();

        if (job == null)
        {
            return Result.Failed();
        }

        job.ObjectState = ObjectState.Deleted;

        foreach (var highlight in job.Highlights)
        {
            highlight.ObjectState = ObjectState.Deleted;
        }

        foreach (var resume in job.Resumes)
        {
            resume.ObjectState = ObjectState.Deleted;
        }

        foreach (var project in job.Projects)
        {
            project.ObjectState = ObjectState.Deleted;

            foreach (var highlight in project.Highlights)
            {
                highlight.ObjectState = ObjectState.Deleted;
            }
        }

        var results = Repository.InsertOrUpdateGraph(job, true);
        if (results > 0)
        {
            return Result.Success();
        }

        return Result.Failed();
    }

    private async Task<int> GetNextJobId(int organizationId)
    {
        var job = await Jobs.AsNoTracking()
            .IgnoreQueryFilters()
            .OrderByDescending(x => x.Id)
            .Where(x => x.OrganizationId == organizationId)
            .FirstOrDefaultAsync();

        if (job == null)
            return 1;

        return job.Id + 1;
    }

}