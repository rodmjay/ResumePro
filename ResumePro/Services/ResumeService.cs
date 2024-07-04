#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper.QueryableExtensions;
using Microsoft.Build.Framework;
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

    public ResumeService(
        IRepositoryAsync<Job> jobRepository,
        IRepositoryAsync<PersonaSkill> personalSkillsRepo,
        IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _jobRepository = jobRepository;
        _personalSkillsRepo = personalSkillsRepo;
    }

    private IQueryable<Resume> Resumes => Repository.Queryable();
    private IQueryable<Job> Jobs => _jobRepository.Queryable();

    private IQueryable<PersonaSkill> PersonalSkills => _personalSkillsRepo.Queryable();

    public Task<T> GetResume<T>(int organizationId, int resumeId) where T : ResumeDto
    {
        return Resumes.Where(x => x.Id == resumeId 
                                  && x.OrganizationId == organizationId)
            .AsNoTracking()
            .ProjectTo<T>(ProjectionMapping)
            .FirstOrDefaultAsync();
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
                    ShowInSummary = true
                });
            }
        }


        var changes = Repository.InsertOrUpdateGraph(resume, true);
        if (changes > 0)
            return await GetResume<ResumeDetails>(organizationId, resume.Id);

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