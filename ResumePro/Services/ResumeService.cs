#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using HandlebarsDotNet;
using Microsoft.EntityFrameworkCore;
using OneOf;
using ResumePro.Core.Data.Enums;
using ResumePro.Core.Data.Interfaces;
using ResumePro.Core.Services.Bases;
using ResumePro.Entities;
using ResumePro.Generation;
using ResumePro.Interfaces;
using ResumePro.Shared;
using ResumePro.Shared.Common;
using ResumePro.Shared.Options;
using System.Globalization;

namespace ResumePro.Services;

public class ResumeService : BaseService<Resume>, IResumeService
{
    static ResumeService()
    {

        Handlebars.RegisterHelper("formatDate", (writer, context, parameters) =>
        {
            if (parameters[0] is DateTime dateTime)
            {
                writer.WriteSafeString(dateTime.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
            }
            else if (DateTime.TryParse(parameters[0]?.ToString(), out DateTime parsedDate))
            {
                writer.WriteSafeString(parsedDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
            }
            else
            {
                writer.WriteSafeString(parameters[0]?.ToString());
            }
        });
    }


    private readonly IRepositoryAsync<Job> _jobRepository;
    private readonly IRepositoryAsync<PersonaSkill> _personalSkillsRepo;
    private readonly IRepositoryAsync<Template> _templateRepo;

    public ResumeService(
        IRepositoryAsync<Job> jobRepository,
        IRepositoryAsync<PersonaSkill> personalSkillsRepo,
        IRepositoryAsync<Template> templateRepo,
        IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _jobRepository = jobRepository;
        _personalSkillsRepo = personalSkillsRepo;
        _templateRepo = templateRepo;
    }

    private IQueryable<Resume> Resumes => Repository.Queryable();
    private IQueryable<Job> Jobs => _jobRepository.Queryable();
    private IQueryable<PersonaSkill> PersonalSkills => _personalSkillsRepo.Queryable();
    private IQueryable<Template> Templates => _templateRepo.Queryable();

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
            Description = options.Description
        };

        if (options.AttachAllJobs)
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

        if (options.AttachAllSkills)
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


        var changes = Repository.InsertOrUpdateGraph(resume, true);
        if (changes > 0)
            return await GetResume<ResumeDetails>(organizationId, personaId, resume.Id);

        return Result.Failed();
    }

    public async Task<OneOf<ResumeDetails, Result>> UpdateResume(int organizationId, int personaId, int resumeId,
        ResumeOptions options)
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
        if (records > 0) return await GetResume<ResumeDetails>(organizationId, 1, resumeId);

        return Result.Failed();
    }

    public async Task<string> SaveResumeAsPdf(int organizationId, int personId, int resumeId)
    {
        var resume = await GetResume<ResumeDetails>(organizationId, personId, resumeId);

        var resumeGenerator = new PdfResumeGenerator(new PdfSettings()
        {
            DisplayInExplorer = false,
            CreateUpdatePdf = true,
            FontFamily = "Verdana"
        });

        return resumeGenerator.ExecuteOperation(resume);
    }

    public async Task<OneOf<GeneratedResume, Result>> Generate(int organizationId, int personId, int resumeId, int templateId)
    {
        var generatedResume = new GeneratedResume();

        var template = await Templates.Where(x => x.Id == templateId)
            .FirstOrDefaultAsync();

        if (template == null || template.Source == null)
            return Result.Failed();

        var resume = await GetResume<ResumeDetails>(organizationId, personId, resumeId);

        if (resume == null)
            return Result.Failed();

        var engine = Handlebars.Compile(template.Source);

        generatedResume.Body = engine(resume);

        return generatedResume;
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