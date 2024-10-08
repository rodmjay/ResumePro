﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using HandlebarsDotNet;
using Microsoft.Extensions.Logging;
using ResumePro.Generation;
using ResumePro.Shared.Models;

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class ResumeService(
    ResumeErrorDescriber resumeErrors,
    TemplateErrorDescriber templateErrors,
    IRepositoryAsync<Job> jobRepo,
    IRepositoryAsync<PersonaSkill> personalSkillsRepo,
    IRepositoryAsync<Rendering> renderingRepo,
    IRepositoryAsync<Template> templateRepo,
    IServiceProvider serviceProvider)
    : BaseService<Resume>(serviceProvider), IResumeService
{
    private IQueryable<Resume> Resumes => Repository.Queryable();
    private IQueryable<Job> Jobs => jobRepo.Queryable();
    private IQueryable<PersonaSkill> PersonalSkills => personalSkillsRepo.Queryable();
    private IQueryable<Template> Templates => templateRepo.Queryable();
    private IQueryable<Rendering> Renderings => renderingRepo.Queryable();

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

    public Task<List<T>> GetResumes<T>(int organizationId, int personId) where T : ResumeDto
    {
        return Resumes.Where(x => x.PersonaId == personId
                                  && x.OrganizationId == organizationId)
            .AsNoTracking()
            .AsSplitQuery()
            .ProjectTo<T>(Mapper)
            .ToListAsync();
    }

    public async Task<OneOf<ResumeDetails, Result>> CreateResume(
        int organizationId, int personId, ResumeOptions options)
    {
        Logger.LogInformation(
            GetLogMessage("OrganizationId: {@organizationId}, PersonId: {@personId}, Options: {@options}"),
            organizationId, personId, options);

        var resume = new Resume
        {
            Id = await GetNextResumeId(organizationId),
            ObjectState = ObjectState.Added,
            OrganizationId = organizationId,
            PersonaId = personId,
            JobTitle = options.JobTitle,
            Description = options.Description,
            ResumeSettings = new ResumeSettings
            {
                ObjectState = ObjectState.Added,
                DefaultTemplateId = options.Settings.DefaultTemplateId,
                OrganizationId = organizationId,
                ResumeYearHistory = options.Settings.ResumeYearHistory,
                ShowTechnologyPerJob = options.Settings.ShowTechnologyPerJob,
                AttachAllJobs = options.Settings.AttachAllJobs,
                AttachAllSkills = options.Settings.AttachAllSkills,
                ShowRatings = options.Settings.ShowRatings,
                ShowContactInfo = options.Settings.ShowContactInfo,
                ShowDuration = options.Settings.ShowDuration,
                SkillView = options.Settings.SkillView
            }
        };


        var changes = Repository.InsertOrUpdateGraph(resume, true);
        if (changes > 0)
        {
            var resumeDto = await GetResume<ResumeDto>(organizationId, personId, resume.Id);

            if (resumeDto.Settings.AttachAllJobs)
            {
                var jobs = await Jobs
                    .AsNoTracking()
                    .Where(x => x.OrganizationId == organizationId && x.PersonaId == personId)
                    .ToListAsync();

                foreach (var job in jobs)
                    resume.Jobs.Add(new ResumeJob
                    {
                        ObjectState = ObjectState.Added,
                        OrganizationId = organizationId,
                        ResumeId = resume.Id,
                        JobId = job.Id
                    });
            }
            else
            {
                if (options.JobIds?.Any() == true)
                {
                    var jobs = await Jobs.AsNoTracking()
                        .Where(x => x.OrganizationId == organizationId && x.PersonaId == personId)
                        .Where(x => options.JobIds.Contains(x.Id))
                        .ToListAsync();

                    foreach (var job in jobs)
                        resume.Jobs.Add(new ResumeJob
                        {
                            ObjectState = ObjectState.Added,
                            JobId = job.Id,
                            ResumeId = resume.Id,
                            OrganizationId = organizationId
                        });
                }
            }


            if (resumeDto.Settings.AttachAllSkills)
            {
                var skills = await PersonalSkills
                    .AsNoTracking()
                    .Where(x => x.OrganizationId == organizationId && x.PersonaId == personId)
                    .ToListAsync();

                foreach (var skill in skills)
                    resume.Skills.Add(new ResumeSkill
                    {
                        ObjectState = ObjectState.Added,
                        OrganizationId = organizationId,
                        SkillId = skill.SkillId
                    });
            }
            else
            {
                if (options.SkillIds?.Any() == true)
                {
                    var skills = await PersonalSkills
                        .AsNoTracking()
                        .Where(x => x.OrganizationId == organizationId && x.PersonaId == personId)
                        .Where(x => options.SkillIds.Contains(x.SkillId))
                        .ToListAsync();

                    foreach (var skill in skills)
                        resume.Skills.Add(new ResumeSkill
                        {
                            ObjectState = ObjectState.Added,
                            SkillId = skill.SkillId,
                            OrganizationId = organizationId
                        });
                }
            }

            var secondaryRecords = Repository.InsertOrUpdateGraph(resume, true);

            var settings = Mapper.Map<ResumeSettingsDto>(resume.ResumeSettings);

            if (settings.DefaultTemplateId > 0) return await Generate(organizationId, personId, resume.Id);

            return await GetResume<ResumeDetails>(organizationId, personId, resume.Id);
        }

        return Result.Failed(resumeErrors.UnableToSaveResume());
    }

    public async Task<OneOf<ResumeDetails, Result>> UpdateResume(int organizationId, int personId, int resumeId,
        ResumeOptions options)
    {
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, ResumeId: {@resumeId}, Options: {@options}"),
            organizationId, personId, resumeId, options);

        var resume = await Resumes
            .Include(x => x.Jobs)
            .Include(x => x.Skills)
            .Include(x => x.ResumeSettings)
            .Where(x => x.OrganizationId == organizationId && x.Id == resumeId && x.PersonaId == personId)
            .FirstOrDefaultAsync();

        if (resume == null)
            return Result.Failed(resumeErrors.ResumeNotFound(resumeId));

        foreach (var job in resume.Jobs) job.ObjectState = ObjectState.Deleted;

        foreach (var skill in resume.Skills) skill.ObjectState = ObjectState.Deleted;


        resume.ObjectState = ObjectState.Modified;
        resume.JobTitle = options.JobTitle;
        resume.Description = options.Description;

        if (resume.ResumeSettings == null)
            resume.ResumeSettings = new ResumeSettings
            {
                ObjectState = ObjectState.Added
            };
        else
            resume.ResumeSettings.ObjectState = ObjectState.Modified;

        resume.ResumeSettings.AttachAllJobs = options.Settings.AttachAllJobs;
        resume.ResumeSettings.AttachAllSkills = options.Settings.AttachAllSkills;
        resume.ResumeSettings.DefaultTemplateId = options.Settings.DefaultTemplateId;
        resume.ResumeSettings.ResumeYearHistory = options.Settings.ResumeYearHistory;
        resume.ResumeSettings.ShowTechnologyPerJob = options.Settings.ShowTechnologyPerJob;
        resume.ResumeSettings.ShowRatings = options.Settings.ShowRatings;
        resume.ResumeSettings.ShowContactInfo = options.Settings.ShowContactInfo;
        resume.ResumeSettings.ShowDuration = options.Settings.ShowDuration;
        resume.ResumeSettings.SkillView = options.Settings.SkillView;


        var allJobIds = await Jobs
            .AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.PersonaId == personId)
            .Select(x => x.Id)
            .ToListAsync();

        var actualJobIds = allJobIds.Where(x => options.JobIds.Contains(x)).ToList();

        foreach (var jobId in actualJobIds)
        {
            var resumeJob = resume.Jobs.FirstOrDefault(x => x.JobId == jobId);
            if (resumeJob == null)
                resume.Jobs.Add(new ResumeJob
                {
                    ObjectState = ObjectState.Added,
                    OrganizationId = organizationId,
                    JobId = jobId
                });
            else
                resumeJob.ObjectState = ObjectState.Unchanged;
        }

        var allSkillIds = await PersonalSkills
            .AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.PersonaId == personId)
            .Select(x => x.SkillId)
            .ToListAsync();

        var actualSkillIds = allSkillIds.Where(x => options.SkillIds.Contains(x)).ToList();

        foreach (var skillId in actualSkillIds)
        {
            var resumeSkill = resume.Skills.FirstOrDefault(x => x.SkillId == skillId);
            if (resumeSkill == null)
                resume.Skills.Add(new ResumeSkill
                {
                    ObjectState = ObjectState.Added,
                    OrganizationId = organizationId,
                    SkillId = skillId
                });
            else
                resumeSkill.ObjectState = ObjectState.Unchanged;
        }


        var records = Repository.InsertOrUpdateGraph(resume, true);
        if (records > 0)
            return await GetResume<ResumeDetails>(organizationId, personId, resumeId);

        return Result.Failed(resumeErrors.UnableToSaveResume());
    }

    public Task<MemoryStream> Generate2(ResumeDetails resume)
    {
        var resumeGenerator = new PdfResumeGenerator(new PdfSettings
        {
            DisplayInExplorer = false,
            CreateUpdatePdf = true,
            FontFamily = "Verdana"
        });

        return Task.FromResult(resumeGenerator.ExecuteOperation(resume));
    }

    public async Task<ResumeDetails> Generate(int organizationId, int personId, int resumeId)
    {
        Logger.LogInformation(
            GetLogMessage("OrganizationId: {@organizationId}, PersonId: {@personId}, ResumeId: {@resumeId}"),
            organizationId, personId, resumeId);

        var resume = await GetResume<ResumeDetails>(organizationId, personId, resumeId);

        var template = await Templates.Where(x => x.Id == resume.Settings.DefaultTemplateId)
            .FirstOrDefaultAsync();

        if (template == null || template.Source == null)
            throw new Exception();

        var rendering = await Renderings.Where(x =>
                x.OrganizationId == organizationId && x.ResumeId == resumeId &&
                x.TemplateId == resume.Settings.DefaultTemplateId)
            .FirstOrDefaultAsync();


        if (rendering == null)
            rendering = new Rendering
            {
                ObjectState = ObjectState.Added,
                OrganizationId = organizationId,
                ResumeId = resumeId,
                TemplateId = resume.Settings.DefaultTemplateId
            };
        else
            rendering.ObjectState = ObjectState.Modified;

        var generatedResume = await Generate(resume, resume.Settings.DefaultTemplateId);
        if (generatedResume.IsT0)
        {
            rendering.Text = generatedResume.AsT0;
            rendering.RenderDate = DateTime.UtcNow;
        }

        var records = renderingRepo.InsertOrUpdateGraph(rendering, true);

        return await GetResume<ResumeDetails>(organizationId, personId, resumeId);
    }

    public async Task<OneOf<string, Result>> Generate(ResumeDetails resume, int templateId)
    {
        Logger.LogInformation(GetLogMessage("Resume: {resume}, TemplateId: {templateId}"),
            resume, templateId);

        var template = await Templates.Where(x => x.Id == templateId)
            .FirstOrDefaultAsync();

        if (template == null || template.Source == null)
            return Result.Failed(templateErrors.TemplateNotFound(templateId));

        var engine = Handlebars.Compile(template.Source);

        return engine(resume);
    }

    public async Task<Result> DeleteResume(int organizationId, int personId, int resumeId)
    {
        Logger.LogInformation(
            GetLogMessage("OrganizationId: {@organizationId}, PersonId: {@personId}, ResumeId: {@resumeId}"),
            organizationId, personId, resumeId);

        var resume = await Resumes
            .Where(x => x.OrganizationId == organizationId && x.Id == resumeId)
            .Include(x => x.Jobs)
            .Include(x => x.Skills)
            .FirstOrDefaultAsync();

        if (resume == null)
            return Result.Failed(resumeErrors.ResumeNotFound(resumeId));

        resume.ObjectState = ObjectState.Deleted;

        foreach (var job in resume.Jobs) job.ObjectState = ObjectState.Deleted;

        foreach (var skill in resume.Skills) skill.ObjectState = ObjectState.Deleted;

        var results = Repository.InsertOrUpdateGraph(resume, true);
        if (results > 0) return Result.Success();

        return Result.Failed();
    }

    private async Task<int> GetNextResumeId(int organizationId)
    {
        var id = await Resumes.AsNoTracking()
            .IgnoreQueryFilters()
            .Where(x => x.OrganizationId == organizationId)
            .OrderByDescending(x => x.Id)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
        return id + 1;
    }
}