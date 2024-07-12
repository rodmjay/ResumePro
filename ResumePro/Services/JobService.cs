#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using OneOf;
using ResumePro.Core.Data.Enums;
using ResumePro.Core.Data.Interfaces;
using ResumePro.Core.Services.Bases;
using ResumePro.Entities;
using ResumePro.Interfaces;
using ResumePro.Shared;
using ResumePro.Shared.Common;
using ResumePro.Shared.Options;

namespace ResumePro.Services;

public class JobService : BaseService<Job>, IJobService
{
    private readonly IRepositoryAsync<Resume> _resumeRepo;

    public JobService(IServiceProvider serviceProvider, IRepositoryAsync<Resume> resumeRepo) : base(serviceProvider)
    {
        _resumeRepo = resumeRepo;
    }

    private IQueryable<Job> Jobs => Repository.Queryable();
    private IQueryable<Resume> Resumes => _resumeRepo.Queryable();

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
        Job job = new Job
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

        int results = Repository.InsertOrUpdateGraph(job, true);
        if (results > 0)
        {
            List<Resume> resumes = await Resumes.Include(x => x.ResumeSettings)
                .Include(x => x.Jobs)
                .ThenInclude(x => x.Job)
                .Where(x => x.PersonaId == personId && x.OrganizationId == organizationId)
                .ToListAsync();

            foreach (Resume resume in resumes)
            {
                if (resume.ResumeSettings is {AttachAllJobs: true})
                {
                    resume.ObjectState = ObjectState.Modified;
                    resume.Jobs.Add(new ResumeJob()
                    {
                        JobId = job.Id,
                        ResumeId = resume.Id,
                        OrganizationId = organizationId,
                        ObjectState = ObjectState.Added
                    });
                }
                _resumeRepo.InsertOrUpdateGraph(resume, false);
            }

            _resumeRepo.Commit();

            return await GetJob<JobDetails>(organizationId, personId, job.Id);
        }

        return Result.Failed();
    }

    public async Task<OneOf<JobDetails, Result>> UpdateJob(int organizationId, int personId, int jobId,
        JobOptions options)
    {
        Job job = await Jobs.Where(x => x.OrganizationId == organizationId && x.PersonaId == personId && x.Id == jobId)
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

        int results = Repository.InsertOrUpdateGraph(job, true);
        if (results > 0) return await GetJob<JobDetails>(organizationId, personId, jobId);

        return Result.Failed();
    }

    public async Task<Result> DeleteJob(int organizationId, int personId, int jobId)
    {
        Job job = await Jobs
            .Include(x => x.Highlights)
            .Include(x => x.Resumes)
            .Include(x => x.Projects)
            .ThenInclude(x => x.Highlights)
            .Include(x => x.Skills)
            .Where(x => x.OrganizationId == organizationId && x.PersonaId == personId && x.Id == jobId)
            .FirstOrDefaultAsync();

        if (job == null) return Result.Failed();

        job.ObjectState = ObjectState.Deleted;

        foreach (Highlight highlight in job.Highlights) highlight.ObjectState = ObjectState.Deleted;

        foreach (ResumeJob resume in job.Resumes) resume.ObjectState = ObjectState.Deleted;

        foreach (Project project in job.Projects)
        {
            project.ObjectState = ObjectState.Deleted;

            foreach (Highlight highlight in project.Highlights) highlight.ObjectState = ObjectState.Deleted;
        }

        int results = Repository.InsertOrUpdateGraph(job, true);
        if (results > 0) return Result.Success();

        return Result.Failed();
    }

    private async Task<int> GetNextJobId(int organizationId)
    {
        Job job = await Jobs.AsNoTracking()
            .IgnoreQueryFilters()
            .OrderByDescending(x => x.Id)
            .Where(x => x.OrganizationId == organizationId)
            .FirstOrDefaultAsync();

        if (job == null)
            return 1;

        return job.Id + 1;
    }
}