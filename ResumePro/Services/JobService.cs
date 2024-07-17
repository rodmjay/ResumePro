#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Models;

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class JobService(IServiceProvider serviceProvider, IRepositoryAsync<Resume> resumeRepo)
    : BaseService<Job>(serviceProvider), IJobService
{
    private IQueryable<Job> Jobs => Repository.Queryable();
    private IQueryable<Resume> Resumes => resumeRepo.Queryable();

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
            var resumes = await Resumes.Include(x => x.ResumeSettings)
                .ThenInclude(x => x.OrganizationSettings)
                .Include(x => x.Jobs)
                .ThenInclude(x => x.Job)
                .Where(x => x.PersonaId == personId && x.OrganizationId == organizationId)
                .ToListAsync();

            foreach (var resume in resumes)
            {
                var settings = Mapper.Map<ResumeSettingsDto>(resume.ResumeSettings);
                if (settings.AttachAllJobs)
                {
                    resume.ObjectState = ObjectState.Modified;
                    resume.Jobs.Add(new ResumeJob
                    {
                        JobId = job.Id,
                        ResumeId = resume.Id,
                        OrganizationId = organizationId,
                        ObjectState = ObjectState.Added
                    });
                }

                resumeRepo.InsertOrUpdateGraph(resume);
            }

            resumeRepo.Commit();

            return await GetJob<JobDetails>(organizationId, personId, job.Id);
        }

        return Result.Failed();
    }

    public async Task<OneOf<JobDetails, Result>> UpdateJob(int organizationId, int personId, int jobId,
        JobOptions options)
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
        if (results > 0) return await GetJob<JobDetails>(organizationId, personId, jobId);

        return Result.Failed();
    }

    public async Task<Result> DeleteJob(int organizationId, int personId, int jobId)
    {
        var job = await Jobs
            .Include(x => x.Highlights)
            .Include(x => x.Resumes)
            .Include(x => x.Projects)
            .ThenInclude(x => x.Highlights)
            .Include(x => x.Skills)
            .Where(x => x.OrganizationId == organizationId && x.PersonaId == personId && x.Id == jobId)
            .FirstOrDefaultAsync();

        if (job == null) return Result.Failed();

        job.ObjectState = ObjectState.Deleted;

        foreach (var highlight in job.Highlights) highlight.ObjectState = ObjectState.Deleted;

        foreach (var resume in job.Resumes) resume.ObjectState = ObjectState.Deleted;

        foreach (var project in job.Projects)
        {
            project.ObjectState = ObjectState.Deleted;

            foreach (var highlight in project.Highlights) highlight.ObjectState = ObjectState.Deleted;
        }

        var results = Repository.InsertOrUpdateGraph(job, true);
        if (results > 0) return Result.Success();

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