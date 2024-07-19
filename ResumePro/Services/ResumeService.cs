#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using HandlebarsDotNet;
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

    public Task<List<T>> GetResumes<T>(int organizationId, int personaId) where T : ResumeDto
    {
        return Resumes.Where(x => x.PersonaId == personaId
                                  && x.OrganizationId == organizationId)
            .AsNoTracking()
            .AsSplitQuery()
            .ProjectTo<T>(Mapper)
            .ToListAsync();
    }

    public async Task<OneOf<ResumeDetails, Result>> CreateResume(
        int organizationId, int personaId, ResumeOptions options)
    {
        var resume = new Resume
        {
            Id = await GetNextResumeId(organizationId),
            ObjectState = ObjectState.Added,
            OrganizationId = organizationId,
            PersonaId = personaId,
            JobTitle = options.Title,
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
            var resumeDto = await GetResume<ResumeDto>(organizationId, personaId, resume.Id);

            if (resumeDto.Settings.AttachAllJobs)
            {
                var jobs = await Jobs
                    .AsNoTracking()
                    .Where(x => x.OrganizationId == organizationId && x.PersonaId == personaId)
                    .ToListAsync();

                foreach (var job in jobs)
                    resume.Jobs.Add(new ResumeJob
                    {
                        ObjectState = ObjectState.Added,
                        JobId = job.Id
                    });
            }
            else
            {
                if (options.JobIds?.Any() == true)
                {
                    var jobs = await Jobs.AsNoTracking()
                        .Where(x => x.OrganizationId == organizationId && x.PersonaId == personaId)
                        .Where(x => options.JobIds.Contains(x.Id))
                        .ToListAsync();

                    foreach (var job in jobs)
                        resume.Jobs.Add(new ResumeJob
                        {
                            ObjectState = ObjectState.Added,
                            JobId = job.Id
                        });
                }
            }
         

            if (resumeDto.Settings.AttachAllSkills)
            {
                var skills = await PersonalSkills
                    .AsNoTracking()
                    .Where(x => x.OrganizationId == organizationId && x.PersonaId == personaId)
                    .ToListAsync();

                foreach (var skill in skills)
                    resume.Skills.Add(new ResumeSkill
                    {
                        ObjectState = ObjectState.Added,
                        SkillId = skill.SkillId
                    });
            }
            else
            {
                if (options.SkillIds?.Any() == true)
                {
                    var skills = await PersonalSkills
                        .AsNoTracking()
                        .Where(x => x.OrganizationId == organizationId && x.PersonaId == personaId)
                        .Where(x => options.SkillIds.Contains(x.SkillId))
                        .ToListAsync();

                    foreach (var skill in skills)
                    {
                        resume.Skills.Add(new ResumeSkill
                        {
                            ObjectState = ObjectState.Added,
                            SkillId = skill.SkillId
                        });
                    }
                }
            }

            var settings = Mapper.Map<ResumeSettingsDto>(resume.ResumeSettings);

            if (settings.DefaultTemplateId != null)
            {
                return await Generate(organizationId, personaId, resume.Id);
            }

            return await GetResume<ResumeDetails>(organizationId, personaId, resume.Id);
        }

        return Result.Failed(resumeErrors.UnableToSaveResume());
    }

    public async Task<OneOf<ResumeDetails, Result>> UpdateResume(int organizationId, int personaId, int resumeId,
        ResumeOptions options)
    {
        var resume = await Resumes
            .Include(x => x.ResumeSettings)
            .Where(x => x.OrganizationId == organizationId && x.Id == resumeId)
            .FirstOrDefaultAsync();

        if (resume == null)
            return Result.Failed(resumeErrors.ResumeNotFound(resumeId));

        resume.ObjectState = ObjectState.Modified;
        resume.JobTitle = options.Title;
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

        var records = Repository.InsertOrUpdateGraph(resume, true);
        if (records > 0)
        {
            return await GetResume<ResumeDetails>(organizationId, 1, resumeId);
        }

        return Result.Failed(resumeErrors.UnableToSaveResume());
    }

    public async Task<string> SaveResumeAsPdf(int organizationId, int personId, int resumeId)
    {
        var resume = await GetResume<ResumeDetails>(organizationId, personId, resumeId);

        var resumeGenerator = new PdfResumeGenerator(new PdfSettings
        {
            DisplayInExplorer = false,
            CreateUpdatePdf = true,
            FontFamily = "Verdana"
        });

        return resumeGenerator.ExecuteOperation(resume);
    }

    public async Task<ResumeDetails> Generate(int organizationId, int personId, int resumeId)
    {
        var resume = await GetResume<ResumeDetails>(organizationId, personId, resumeId);

        var template = await Templates.Where(x => x.Id == resume.Settings.DefaultTemplateId)
            .FirstOrDefaultAsync();

        if (template == null || template.Source == null)
            throw new Exception();

        var rendering = await Renderings.Where(x =>
                x.OrganizationId == organizationId && x.ResumeId == resumeId && x.TemplateId == resume.Settings.DefaultTemplateId)
            .FirstOrDefaultAsync();


        if (rendering == null)
        {
            rendering = new Rendering()
            {
                ObjectState = ObjectState.Added,
                OrganizationId = organizationId,
                ResumeId = resumeId,
                TemplateId = resume.Settings.DefaultTemplateId
            };
        }
        else
        {
            rendering.ObjectState = ObjectState.Modified;
        }

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
        var template = await Templates.Where(x => x.Id == templateId)
            .FirstOrDefaultAsync();

        if (template == null || template.Source == null)
            return Result.Failed(templateErrors.TemplateNotFound(templateId));

        var engine = Handlebars.Compile(template.Source);
        
        return engine(resume);
    }

    public async Task<Result> DeleteResume(int organizationId, int personaId, int resumeId)
    {
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