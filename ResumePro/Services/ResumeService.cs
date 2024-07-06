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

public class ResumeService : BaseService<Resume>, IResumeService
{
    private readonly IRepositoryAsync<Job> _jobRepository;
    private readonly IRepositoryAsync<PersonaSkill> _personalSkillsRepo;
    private readonly IRepositoryAsync<Reference> _referencesRepo;

    public ResumeService(
        IRepositoryAsync<Job> jobRepository,
        IRepositoryAsync<PersonaSkill> personalSkillsRepo,
        IRepositoryAsync<Reference> referencesRepo,
        IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _jobRepository = jobRepository;
        _personalSkillsRepo = personalSkillsRepo;
        _referencesRepo = referencesRepo;
    }

    private IQueryable<Resume> Resumes => Repository.Queryable();
    private IQueryable<Job> Jobs => _jobRepository.Queryable();

    private IQueryable<PersonaSkill> PersonalSkills => _personalSkillsRepo.Queryable();

    public async Task<T> GetResume<T>(int organizationId, int personId, int resumeId) where T : ResumeDto
    {
        var resume = await Resumes.Where(x => x.Id == resumeId && x.PersonaId == personId
                                  && x.OrganizationId == organizationId)
            .AsNoTracking()
            .AsSplitQuery()
            .ProjectTo<T>(Mapper)
            .FirstOrDefaultAsync();

        return resume;
    }

    public Task<List<T>> GetResumes<T>(int organizationId, int personaId) where T : ResumeDto
    {
        return Resumes.Where(x => x.PersonaId == personaId
                                  && x.OrganizationId == organizationId)
            .AsNoTracking()
            .ProjectTo<T>(Mapper)
            .ToListAsync();
    }

    public async Task<OneOf<ResumeDetails, Result>> CreateResume(
        int organizationId, int personaId, CreateResumeOptions options)
    {
        var resume = new Resume()
        {
            Id = await GetNextResumeId(organizationId),
            ObjectState = ObjectState.Added,
            OrganizationId = organizationId,
            PersonaId = personaId,
            JobTitle = options.Title,
            Description = options.Description
        };

        if (options.AttachAllJobs)
        {
            var jobs = await Jobs
                .AsNoTracking()
                .Where(x => x.OrganizationId == organizationId && x.PersonaId == personaId)
                .ToListAsync();

            foreach (var job in jobs)
            {
                resume.Jobs.Add(new ResumeJob()
                {
                    ObjectState = ObjectState.Added,
                    JobId = job.Id
                });
            }
        }

        if (options.AttachAllSkills)
        {
            var skills = await PersonalSkills
                .AsNoTracking()
                .Where(x => x.OrganizationId == organizationId && x.PersonaId == personaId)
                .ToListAsync();

            foreach (var skill in skills)
            {
                resume.Skills.Add(new ResumeSkill()
                {
                    ObjectState = ObjectState.Added,
                    SkillId = skill.SkillId,
                });
            }
        }


        var changes = Repository.InsertOrUpdateGraph(resume, true);
        if (changes > 0)
            return await GetResume<ResumeDetails>(organizationId, personaId, resume.Id);

        return Result.Failed();
    }

    public async Task<OneOf<ResumeDetails, Result>> UpdateResume(int organizationId, int personaId, int resumeId, CreateResumeOptions options)
    {
        var resume = await Resumes
            .Where(x => x.OrganizationId == organizationId && x.Id == resumeId)
            .FirstOrDefaultAsync();

        if (resume == null)
            return Result.Failed();

        resume.ObjectState = ObjectState.Modified;
        resume.JobTitle = options.Title;
        resume.Description = options.Description;

        var records = Repository.InsertOrUpdateGraph(resume, true);
        if (records > 0)
        {
            return await GetResume<ResumeDetails>(organizationId, 1, resumeId);
        }

        return Result.Failed();
    }

    public async Task<Result> DeleteResume(int organizationId, int personaId, int resumeId)
    {
        var resume = await Resumes
            .Where(x => x.OrganizationId == organizationId && x.Id == resumeId)
            .Include(x => x.Jobs)
            .Include(x => x.Skills)
            .FirstOrDefaultAsync();

        if (resume == null)
            return Result.Failed();

        resume.ObjectState = ObjectState.Deleted;

        foreach (var job in resume.Jobs)
        {
            job.ObjectState = ObjectState.Deleted;
        }

        foreach (var skill in resume.Skills)
        {
            skill.ObjectState = ObjectState.Deleted;
        }

        var results = Repository.InsertOrUpdateGraph(resume, true);
        if (results > 0)
        {
            return Result.Success();
        }

        return Result.Failed();
    }

    private async Task<int> GetNextResumeId(int organizationId)
    {
        var resume = await Resumes.AsNoTracking()
            .IgnoreQueryFilters()
            .Where(x => x.OrganizationId == organizationId)
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync();

        if (resume == null)
            return 1;

        return resume.Id + 1;
    }
}