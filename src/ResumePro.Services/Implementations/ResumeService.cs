using System.Diagnostics.CodeAnalysis;
using Bespoke.Core;
using Bespoke.Data.Enums;
using Bespoke.Data.Extensions;
using Bespoke.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResumePro.Services.ErrorDescribers;
using ResumePro.Services.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Services.Implementations;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class ResumeService : BaseService<Resume>, IResumeService
{
    private readonly IIdGenerationService _idGenerationService;
    private readonly IRepositoryAsync<Company> _jobRepo;
    private readonly IRepositoryAsync<PersonaSkill> _personalSkillsRepo;
    private readonly ResumeErrorDescriber _resumeErrors;
    private readonly TemplateErrorDescriber _templateErrors;

    public ResumeService(ResumeErrorDescriber resumeErrors,
        TemplateErrorDescriber templateErrors,
        IRepositoryAsync<Company> jobRepo,
        IRepositoryAsync<PersonaSkill> personalSkillsRepo,
        IRepositoryAsync<Rendering> renderingRepo,
        IRepositoryAsync<Template> templateRepo,
        IIdGenerationService idGenerationService,
        IEventAggregator eventAggregator,
        IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _resumeErrors = resumeErrors;
        _templateErrors = templateErrors;
        _jobRepo = jobRepo;
        _personalSkillsRepo = personalSkillsRepo;
        _idGenerationService = idGenerationService;
    }

    private IQueryable<Resume> Resumes => Repository.Queryable();
    private IQueryable<Company> Companies => _jobRepo.Queryable();
    private IQueryable<PersonaSkill> PersonalSkills => _personalSkillsRepo.Queryable();

    public async Task<T> GetResume<T>(int organizationId, int personId, int resumeId) where T : ResumeDto
    {
        var resume = await Resumes.Where(x => x.Id == resumeId && x.PersonId == personId
                                                               && x.OrganizationId == organizationId)
            .AsNoTracking()
            .AsSplitQuery()
            .ProjectTo<T>(Mapper)
            .FirstAsync();

        return resume;
    }

    public Task<List<T>> GetResumes<T>(int organizationId, int personId) where T : ResumeDto
    {
        return Resumes.Where(x => x.PersonId == personId
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
            Id = await _idGenerationService.GetNextResumeId(organizationId),
            ObjectState = ObjectState.Added,
            OrganizationId = organizationId,
            PersonId = personId,
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
                var jobs = await Companies
                    .AsNoTracking()
                    .Where(x => x.OrganizationId == organizationId && x.PersonId == personId)
                    .ToListAsync();

                foreach (var job in jobs)
                    resume.Companies.Add(new ResumeCompany
                    {
                        ObjectState = ObjectState.Added,
                        OrganizationId = organizationId,
                        ResumeId = resume.Id,
                        CompanyId = job.Id
                    });
            }
            else
            {
                if (options.CompanyIds?.Any() == true)
                {
                    var jobs = await Companies.AsNoTracking()
                        .Where(x => x.OrganizationId == organizationId && x.PersonId == personId)
                        .Where(x => options.CompanyIds.Contains(x.Id))
                        .ToListAsync();

                    foreach (var job in jobs)
                        resume.Companies.Add(new ResumeCompany
                        {
                            ObjectState = ObjectState.Added,
                            CompanyId = job.Id,
                            ResumeId = resume.Id,
                            OrganizationId = organizationId
                        });
                }
            }


            if (resumeDto.Settings.AttachAllSkills)
            {
                var skills = await PersonalSkills
                    .AsNoTracking()
                    .Where(x => x.OrganizationId == organizationId && x.PersonId == personId)
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
                        .Where(x => x.OrganizationId == organizationId && x.PersonId == personId)
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

            //if (settings.DefaultTemplateId > 0) return await GetResume<ResumeDd(organizationId, personId, resume.Id);

            return await GetResume<ResumeDetails>(organizationId, personId, resume.Id);
        }

        return Result.Failed(_resumeErrors.UnableToSaveResume());
    }

    public async Task<OneOf<ResumeDetails, Result>> UpdateResume(int organizationId, int personId, int resumeId,
        ResumeOptions options)
    {
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, ResumeId: {@resumeId}, Options: {@options}"),
            organizationId, personId, resumeId, options);

        var resume = await Resumes
            .Include(x => x.Companies)
            .Include(x => x.Skills)
            .Include(x => x.ResumeSettings)
            .Where(x => x.OrganizationId == organizationId && x.Id == resumeId && x.PersonId == personId)
            .FirstOrDefaultAsync();

        if (resume == null)
            return Result.Failed(_resumeErrors.ResumeNotFound(resumeId));

        foreach (var job in resume.Companies) job.ObjectState = ObjectState.Deleted;

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


        var allJobIds = await Companies
            .AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.PersonId == personId)
            .Select(x => x.Id)
            .ToListAsync();

        var actualJobIds = allJobIds.Where(x => options.CompanyIds.Contains(x)).ToList();

        foreach (var companyId in actualJobIds)
        {
            var resumeJob = resume.Companies.FirstOrDefault(x => x.CompanyId == companyId);
            if (resumeJob == null)
                resume.Companies.Add(new ResumeCompany
                {
                    ObjectState = ObjectState.Added,
                    OrganizationId = organizationId,
                    CompanyId = companyId
                });
            else
                resumeJob.ObjectState = ObjectState.Unchanged;
        }

        var allSkillIds = await PersonalSkills
            .AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.PersonId == personId)
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

        return Result.Failed(_resumeErrors.UnableToSaveResume());
    }

    public Task<MemoryStream> Generate2(ResumeDetails resume)
    {
        throw new NotImplementedException();
        //var resumeGenerator = new PdfResumeGenerator(new PdfSettings
        //{
        //    DisplayInExplorer = false,
        //    CreateUpdatePdf = true,
        //    FontFamily = "Verdana"
        //});

        //return Task.FromResult(resumeGenerator.ExecuteOperation(resume));
    }

    //public async Task<ResumeDetails> Generate(int organizationId, int personId, int resumeId)
    //{
    //    Logger.LogInformation(
    //        GetLogMessage("OrganizationId: {@organizationId}, PersonId: {@personId}, ResumeId: {@resumeId}"),
    //        organizationId, personId, resumeId);

    //    var resume = await GetResume<ResumeDetails>(organizationId, personId, resumeId);

    //    var template = await Templates.Where(x => x.Id == resume.Settings.DefaultTemplateId)
    //        .FirstOrDefaultAsync();

    //    if (template == null || template.Source == null)
    //        throw new Exception();

    //    var rendering = await Renderings.Where(x =>
    //            x.OrganizationId == organizationId && x.ResumeId == resumeId &&
    //            x.TemplateId == resume.Settings.DefaultTemplateId)
    //        .FirstOrDefaultAsync();


    //    if (rendering == null)
    //        rendering = new Rendering
    //        {
    //            ObjectState = ObjectState.Added,
    //            OrganizationId = organizationId,
    //            ResumeId = resumeId,
    //            TemplateId = resume.Settings.DefaultTemplateId
    //        };
    //    else
    //        rendering.ObjectState = ObjectState.Modified;

    //    var generatedResume = await Generate(resume, resume.Settings.DefaultTemplateId);
    //    if (generatedResume.IsT0)
    //    {
    //        rendering.Text = generatedResume.AsT0;
    //        rendering.RenderDate = DateTime.UtcNow;
    //    }

    //    var records = _renderingRepo.InsertOrUpdateGraph(rendering, true);

    //    return await GetResume<ResumeDetails>(organizationId, personId, resumeId);
    //}

    //public async Task<OneOf<string, Result>> Generate(ResumeDetails resume, int templateId)
    //{
    //    Logger.LogInformation(GetLogMessage("Resume: {resume}, TemplateId: {templateId}"),
    //        resume, templateId);

    //    var template = await Templates.Where(x => x.Id == templateId)
    //        .FirstOrDefaultAsync();

    //    if (template == null || template.Source == null)
    //        return Result.Failed(_templateErrors.TemplateNotFound(templateId));

    //    var engine = Handlebars.Compile(template.Source);

    //    return engine(resume);
    //}

    public async Task<Result> DeleteResume(int organizationId, int personId, int resumeId)
    {
        Logger.LogInformation(
            GetLogMessage("OrganizationId: {@organizationId}, PersonId: {@personId}, ResumeId: {@resumeId}"),
            organizationId, personId, resumeId);

        var resume = await Resumes
            .Where(x => x.OrganizationId == organizationId && x.Id == resumeId)
            .Include(x => x.Companies)
            .Include(x => x.Skills)
            .FirstOrDefaultAsync();

        if (resume == null)
            return Result.Failed(_resumeErrors.ResumeNotFound(resumeId));

        resume.ObjectState = ObjectState.Deleted;

        foreach (var job in resume.Companies) job.ObjectState = ObjectState.Deleted;

        foreach (var skill in resume.Skills) skill.ObjectState = ObjectState.Deleted;

        var results = Repository.InsertOrUpdateGraph(resume, true);
        if (results > 0) return Result.Success();

        return Result.Failed();
    }
}