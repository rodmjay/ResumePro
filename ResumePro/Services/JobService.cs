﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.Extensions.Logging;
using ResumePro.Shared.Models;

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class JobService(
    IServiceProvider serviceProvider,
    IRepositoryAsync<Project> projectRepo,
    IRepositoryAsync<Resume> resumeRepo,
    IRepositoryAsync<PersonaSkill> personSkillRepo,
    IRepositoryAsync<Highlight> highlightRepo)
    : BaseService<Job>(serviceProvider), IJobService
{
    private IQueryable<Job> Jobs => Repository.Queryable();
    private IQueryable<Resume> Resumes => resumeRepo.Queryable();
    private IQueryable<Highlight> Highlights => highlightRepo.Queryable();
    private IQueryable<Project> Projects => projectRepo.Queryable();
    private IQueryable<PersonaSkill> PersonSkills => personSkillRepo.Queryable();

    public Task<List<T>> GetJobs<T>(int organizationId, int personId) where T : JobDto
    {
        return Jobs.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.PersonaId == personId)
            .ProjectTo<T>(Mapper).ToListAsync();
    }

    public Task<T> GetJob<T>(int organizationId, int personId, int jobId) where T : JobDto
    {
        return Jobs.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.PersonaId == personId && x.Id == jobId)
            .ProjectTo<T>(Mapper).FirstOrDefaultAsync();
    }

    public async Task<OneOf<JobDetails, Result>> CreateJob(int organizationId, int personId, JobOptions options)
    {
        Logger.LogInformation(
            GetLogMessage("OrganizationId: {@organizationId}, PersonId: {@personId}, Options: {@options}"),
            organizationId, personId, options);

        var job = new Job
        {
            Id = await GetNextJobId(organizationId),
            ObjectState = ObjectState.Added,
            StartDate = options.StartDate,
            JobTitle = options.JobTitle,
            EndDate = options.EndDate,
            Company = options.Company,
            Description = options.Description,
            Location = options.Location,
            OrganizationId = organizationId,
            PersonaId = personId
        };

        var personSkills = await PersonSkills.Where(x => x.OrganizationId == organizationId && x.PersonaId == personId)
            .Select(x => x.SkillId).ToListAsync();

        var availableSkills = options.JobSkillIds.Where(x => personSkills.Contains(x)).ToList();


        var nextHighlightId = await GetNextHighlightId(organizationId);
        var nextProjectId = await GetNextProjectId(organizationId);

        foreach (var availableSkill in availableSkills)
            job.Skills.Add(new JobSkill
            {
                ObjectState = ObjectState.Added,
                SkillId = availableSkill,
                PersonaId = personId,
                OrganizationId = organizationId
            });

        for (var index = 0; index < options.ProjectOptions.Count; index++)
        {
            var project = options.ProjectOptions[index];
            var projectEntity = new Project
            {
                OrganizationId = organizationId,
                Budget = project.Budget,
                Description = project.Description,
                Id = nextProjectId++,
                Name = project.Name,
                Order = index + 1,
                ObjectState = ObjectState.Added
            };

            for (var i = 0; i < project.HighlightOptions.Count; i++)
            {
                var highlight = project.HighlightOptions[i];
                projectEntity.Highlights.Add(new Highlight
                {
                    OrganizationId = organizationId,
                    ObjectState = ObjectState.Added,
                    Id = nextHighlightId++,
                    Text = highlight.Text,
                    Order = i + 1
                });
            }

            job.Projects.Add(projectEntity);
        }

        for (var index = 0; index < options.HighlightOptions.Count; index++)
        {
            var highlight = options.HighlightOptions[index];
            job.Highlights.Add(new Highlight
            {
                Id = nextHighlightId++,
                ObjectState = ObjectState.Added,
                Order = index,
                Text = highlight.Text,
                OrganizationId = organizationId
            });
        }


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
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, JobId: {@jobId}, Options: {@options}"),
            organizationId, personId, jobId, options);

        var job = await Jobs.Include(x => x.Highlights)
            .Include(x => x.Projects)
            .ThenInclude(x => x.Highlights)
            .Include(x => x.Skills)
            .Where(x => x.OrganizationId == organizationId && x.PersonaId == personId && x.Id == jobId)
            .FirstOrDefaultAsync();

        if (job == null)
            return Result.Failed();

        job.ObjectState = ObjectState.Modified;
        job.Company = options.Company;
        job.Description = options.Description;
        job.EndDate = options.EndDate;
        job.StartDate = options.StartDate;
        job.Location = options.Location;
        job.JobTitle = options.JobTitle;

        var nextHighlightId = await GetNextHighlightId(organizationId);
        var nextProjectId = await GetNextProjectId(organizationId);

        foreach (var highlight in job.Highlights.Where(a => a.ProjectId == null))
            highlight.ObjectState = ObjectState.Deleted;

        foreach (var project in job.Projects)
        {
            project.ObjectState = ObjectState.Deleted;
            foreach (var highlight in project.Highlights) highlight.ObjectState = ObjectState.Deleted;
        }

        foreach (var skill in job.Skills) skill.ObjectState = ObjectState.Deleted;

        foreach (var skillId in options.JobSkillIds)
        {
            var skill = job.Skills.FirstOrDefault(x => x.SkillId == skillId);
            if (skill == null)
                job.Skills.Add(new JobSkill
                {
                    ObjectState = ObjectState.Added,
                    PersonaId = personId,
                    OrganizationId = organizationId,
                    SkillId = skillId
                });
            else
                skill.ObjectState = ObjectState.Unchanged;
        }

        for (var index = 0; index < options.HighlightOptions.Count; index++)
        {
            var highlight = options.HighlightOptions[index];

            var highlightEntity = job.Highlights.FirstOrDefault(x => x.Id == highlight.Id && x.ProjectId == null);
            if (highlightEntity == null)
            {
                highlightEntity = new Highlight
                {
                    ObjectState = ObjectState.Added,
                    Id = nextHighlightId++,
                    Order = index + 1,
                    OrganizationId = organizationId
                };
                job.Highlights.Add(highlightEntity);
            }
            else
            {
                highlightEntity.ObjectState = ObjectState.Modified;
                highlightEntity.Order = index + 1;
            }

            highlightEntity.Text = highlight.Text;
        }

        for (var index = 0; index < options.ProjectOptions.Count; index++)
        {
            var projectOptions = options.ProjectOptions[index];
            var projectEntity = job.Projects.FirstOrDefault(x => x.Id == projectOptions.Id);
            if (projectEntity == null)
            {
                projectEntity = new Project
                {
                    ObjectState = ObjectState.Added,
                    Id = nextProjectId++,
                    Order = job.Projects.Count + 1,
                    OrganizationId = organizationId
                };
                job.Projects.Add(projectEntity);
            }
            else
            {
                projectEntity.ObjectState = ObjectState.Modified;
                projectEntity.Order = index + 1;
            }

            projectEntity.Name = projectOptions.Name;
            projectEntity.Description = projectOptions.Description;
            projectEntity.Budget = projectOptions.Budget;

            for (var i = 0; i < projectOptions.HighlightOptions.Count; i++)
            {
                var highlightOption = projectOptions.HighlightOptions[i];
                var highlightEntity = projectEntity.Highlights.FirstOrDefault(x =>
                    x.Id == highlightOption.Id && x.ProjectId == projectOptions.Id);
                if (highlightEntity == null)
                {
                    highlightEntity = new Highlight
                    {
                        ObjectState = ObjectState.Added,
                        Id = nextHighlightId++,
                        ProjectId = projectEntity.Id,
                        OrganizationId = organizationId
                    };
                    projectEntity.Highlights.Add(highlightEntity);
                }
                else
                {
                    highlightEntity.ObjectState = ObjectState.Modified;
                }

                highlightEntity.Text = highlightOption.Text;
                highlightEntity.Order = i + 1;
            }
        }

        var results = Repository.InsertOrUpdateGraph(job, true);
        if (results > 0) return await GetJob<JobDetails>(organizationId, personId, jobId);

        return Result.Failed();
    }

    public async Task<Result> DeleteJob(int organizationId, int personId, int jobId)
    {
        Logger.LogInformation(GetLogMessage("OrganizationId: {@organizationId}, PersonId: {@personId}, JobId: {jobId}"),
            organizationId, personId, jobId);

        var job = await Jobs
            .Include(x => x.Highlights)
            .Include(x => x.Projects)
            .Where(x => x.OrganizationId == organizationId && x.PersonaId == personId && x.Id == jobId)
            .FirstOrDefaultAsync();

        if (job == null) return Result.Failed();

        foreach (var highlight in job.Highlights) highlightRepo.Delete(highlight);

        foreach (var project in job.Projects) projectRepo.Delete(project);

        Repository.Delete(job);

        var results = UnitOfWork.SaveChanges();
        if (results > 0) return Result.Success();

        return Result.Failed();
    }

    private async Task<int> GetNextJobId(int organizationId)
    {
        var id = await Jobs.AsNoTracking()
            .IgnoreQueryFilters()
            .Where(x => x.OrganizationId == organizationId)
            .OrderByDescending(x => x.Id)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
        return id + 1;
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

    private async Task<int> GetNextHighlightId(int organizationId)
    {
        var id = await Highlights.AsNoTracking()
            .IgnoreQueryFilters()
            .Where(x => x.OrganizationId == organizationId)
            .OrderByDescending(x => x.Id)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
        return id + 1;
    }
}