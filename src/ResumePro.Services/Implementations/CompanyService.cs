using System.Diagnostics.CodeAnalysis;
using Bespoke.Data.Enums;
using Bespoke.Data.Extensions;
using Bespoke.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResumePro.Services.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Services.Implementations;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class CompanyService : BaseService<Company>, ICompanyService
{
    private readonly IRepositoryAsync<Highlight> _highlightRepo;
    private readonly IRepositoryAsync<PersonaSkill> _personSkillRepo;
    private readonly IRepositoryAsync<Position> _positionRepo;
    private readonly IRepositoryAsync<Project> _projectRepo;
    private readonly IRepositoryAsync<Resume> _resumeRepo;

    public CompanyService(IServiceProvider serviceProvider,
        IRepositoryAsync<Project> projectRepo,
        IRepositoryAsync<Resume> resumeRepo,
        IRepositoryAsync<PersonaSkill> personSkillRepo,
        IRepositoryAsync<Position> positionRepo,
        IRepositoryAsync<Highlight> highlightRepo) : base(serviceProvider)
    {
        _projectRepo = projectRepo;
        _resumeRepo = resumeRepo;
        _personSkillRepo = personSkillRepo;
        _positionRepo = positionRepo;
        _highlightRepo = highlightRepo;
    }

    private IQueryable<Company> Companies => Repository.Queryable();
    private IQueryable<Resume> Resumes => _resumeRepo.Queryable();
    private IQueryable<Highlight> Highlights => _highlightRepo.Queryable();
    private IQueryable<Project> Projects => _projectRepo.Queryable();
    private IQueryable<PersonaSkill> PersonSkills => _personSkillRepo.Queryable();
    private IQueryable<Position> Positions => _positionRepo.Queryable();

    public Task<List<T>> GetCompanies<T>(int organizationId, int personId) where T : CompanyDto
    {
        return Companies.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.PersonId == personId)
            .ProjectTo<T>(Mapper).ToListAsync();
    }

    public Task<T> GetCompany<T>(int organizationId, int personId, int companyId) where T : CompanyDto
    {
        return Companies.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.PersonId == personId && x.Id == companyId)
            .ProjectTo<T>(Mapper).FirstAsync();
    }

    public async Task<OneOf<CompanyDetails, Result>> CreateCompany(int organizationId, int personId,
        CompanyOptions options)
    {
        Logger.LogInformation(
            GetLogMessage("OrganizationId: {@organizationId}, PersonId: {@personId}, Options: {@options}"),
            organizationId, personId, options);

        var company = new Company
        {
            Id = await GetNextJobId(organizationId),
            ObjectState = ObjectState.Added,
            StartDate = options.StartDate,
            EndDate = options.EndDate,
            CompanyName = options.Company,
            Description = options.Description,
            Location = options.Location,
            OrganizationId = organizationId,
            PersonId = personId
        };

        var personSkills = await PersonSkills.Where(x => x.OrganizationId == organizationId && x.PersonId == personId)
            .Select(x => x.SkillId).ToListAsync();

        var availableSkills = options.JobSkillIds.Where(x => personSkills.Contains(x)).ToList();

        foreach (var availableSkill in availableSkills)
            company.Skills.Add(new CompanySkill
            {
                ObjectState = ObjectState.Added,
                SkillId = availableSkill,
                PersonId = personId,
                OrganizationId = organizationId
            });


        for (var i = 0; i < options.Positions.Count; i++)
        {
            var positionOptions = options.Positions[i];

            var ent = new Position
            {
                Id = i + 1,
                StartDate = positionOptions.StartDate,
                EndDate = positionOptions.EndDate,
                ObjectState = ObjectState.Added,
                JobTitle = positionOptions.JobTitle
            };

            for (var j = 0; j < positionOptions.Highlights.Count; j++)
            {
                var highlight = positionOptions.Highlights[j];
                ent.Highlights.Add(new Highlight
                {
                    Id = j + 1,
                    ObjectState = ObjectState.Added,
                    Order = j + 1,
                    Text = highlight.Text,
                    OrganizationId = organizationId
                });
            }

            for (var k = 0; k < positionOptions.Projects.Count; k++)
            {
                var projectOptions = positionOptions.Projects[k];
                var project = new Project
                {
                    ObjectState = ObjectState.Added,
                    Order = k + 1,
                    Id = k + 1,
                    Name = projectOptions.Name,
                    Description = projectOptions.Description,
                    Budget = projectOptions.Budget
                };

                for (var l = 0; l < projectOptions.Highlights.Count; l++)
                {
                    var highlightOptions = projectOptions.Highlights[l];
                    var highlight = new ProjectHighlight
                    {
                        ObjectState = ObjectState.Added,
                        Id = l + 1,
                        Order = l + 1,
                        Text = highlightOptions.Text
                    };

                    project.Highlights.Add(highlight);
                }

                ent.Projects.Add(project);
            }

            company.Positions.Add(ent);
        }


        var results = Repository.InsertOrUpdateGraph(company, true);
        if (results > 0)
        {
            var resumes = await Resumes.Include(x => x.ResumeSettings)
                .ThenInclude(x => x.OrganizationSettings)
                .Include(x => x.Companies)
                .ThenInclude(x => x.Company)
                .Where(x => x.PersonId == personId && x.OrganizationId == organizationId)
                .ToListAsync();

            foreach (var resume in resumes)
            {
                var settings = Mapper.Map<ResumeSettingsDto>(resume.ResumeSettings);
                if (settings.AttachAllJobs)
                {
                    resume.ObjectState = ObjectState.Modified;
                    resume.Companies.Add(new ResumeCompany
                    {
                        CompanyId = company.Id,
                        ResumeId = resume.Id,
                        OrganizationId = organizationId,
                        ObjectState = ObjectState.Added
                    });
                }

                _resumeRepo.InsertOrUpdateGraph(resume);
            }

            _resumeRepo.Commit();

            return await GetCompany<CompanyDetails>(organizationId, personId, company.Id);
        }

        return Result.Failed();
    }

    public async Task<OneOf<CompanyDetails, Result>> UpdateCompany(int organizationId, int personId, int companyId,
        CompanyOptions options)
    {
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, CompanyId: {@companyId}, Options: {@options}"),
            organizationId, personId, companyId, options);

        var company = await Companies.Include(x => x.Positions)
            .ThenInclude(x => x.Highlights)
            .Include(x => x.Projects)
            .ThenInclude(x => x.Highlights)
            .Include(x => x.Skills)
            .Where(x => x.OrganizationId == organizationId && x.PersonId == personId && x.Id == companyId)
            .FirstOrDefaultAsync();

        if (company == null)
            return Result.Failed();

        company.ObjectState = ObjectState.Modified;
        company.CompanyName = options.Company;
        company.Description = options.Description;
        company.EndDate = options.EndDate;
        company.StartDate = options.StartDate;
        company.Location = options.Location;

        var nextHighlightId = await GetNextHighlightId(organizationId);
        var nextProjectId = await GetNextProjectId(organizationId);
        var nextPositionId = await GetNextPositionId(organizationId);

        foreach (var position in company.Positions)
        {
            position.ObjectState = ObjectState.Deleted;
            foreach (var highlight in position.Highlights)
                highlight.ObjectState = ObjectState.Deleted;
        }

        foreach (var project in company.Projects)
        {
            project.ObjectState = ObjectState.Deleted;
            foreach (var highlight in project.Highlights) highlight.ObjectState = ObjectState.Deleted;
        }

        foreach (var skill in company.Skills) skill.ObjectState = ObjectState.Deleted;

        foreach (var skillId in options.JobSkillIds)
        {
            var skill = company.Skills.FirstOrDefault(x => x.SkillId == skillId);
            if (skill == null)
                company.Skills.Add(new CompanySkill
                {
                    ObjectState = ObjectState.Added,
                    PersonId = personId,
                    OrganizationId = organizationId,
                    SkillId = skillId
                });
            else
                skill.ObjectState = ObjectState.Unchanged;
        }

        for (var i = 0; i < options.Positions.Count; i++)
        {
            var positionOptions = options.Positions[i];
            // todo: add stuff here
            var position = company.Positions.FirstOrDefault(x => x.Id == positionOptions.Id);
            if (position == null)
                position = new Position
                {
                    ObjectState = ObjectState.Added,
                    CompanyId = companyId,
                    PersonId = personId,
                    Id = nextPositionId++
                };
            else
                position.ObjectState = ObjectState.Modified;

            position.StartDate = positionOptions.StartDate;
            position.EndDate = positionOptions.EndDate;
            position.JobTitle = positionOptions.JobTitle;

            for (var index = 0; index < positionOptions.Highlights.Count; index++)
            {
                var highlight = positionOptions.Highlights[index];

                var highlightEntity = position.Highlights.FirstOrDefault(x => x.Id == highlight.Id);
                if (highlightEntity == null)
                {
                    highlightEntity = new Highlight
                    {
                        ObjectState = ObjectState.Added,
                        Id = nextHighlightId++,
                        Order = index + 1,
                        OrganizationId = organizationId
                    };
                    position.Highlights.Add(highlightEntity);
                }
                else
                {
                    highlightEntity.ObjectState = ObjectState.Modified;
                    highlightEntity.Order = index + 1;
                }

                highlightEntity.Text = highlight.Text;
            }
        }


        //for (var index = 0; index < options.ProjectOptions.Count; index++)
        //{
        //    var projectOptions = options.ProjectOptions[index];
        //    var projectEntity = company.Projects.FirstOrDefault(x => x.Id == projectOptions.Id);
        //    if (projectEntity == null)
        //    {
        //        projectEntity = new Project
        //        {
        //            ObjectState = ObjectState.Added,
        //            Id = nextProjectId++,
        //            Order = company.Projects.Count + 1,
        //            OrganizationId = organizationId
        //        };
        //        company.Projects.Add(projectEntity);
        //    }
        //    else
        //    {
        //        projectEntity.ObjectState = ObjectState.Modified;
        //        projectEntity.Order = index + 1;
        //    }

        //    projectEntity.Name = projectOptions.Name;
        //    projectEntity.Description = projectOptions.Description;
        //    projectEntity.Budget = projectOptions.Budget;

        //    for (var i = 0; i < projectOptions.HighlightOptions.Count; i++)
        //    {
        //        var highlightOption = projectOptions.HighlightOptions[i];
        //        var highlightEntity = projectEntity.Highlights.FirstOrDefault(x =>
        //            x.Id == highlightOption.Id && x.ProjectId == projectOptions.Id);
        //        if (highlightEntity == null)
        //        {
        //            highlightEntity = new ProjectHighlight
        //            {
        //                ObjectState = ObjectState.Added,
        //                Id = nextHighlightId++,
        //                OrganizationId = organizationId
        //            };
        //            projectEntity.Highlights.Add(highlightEntity);
        //        }
        //        else
        //        {
        //            highlightEntity.ObjectState = ObjectState.Modified;
        //        }

        //        highlightEntity.Text = highlightOption.Text;
        //        highlightEntity.Order = i + 1;
        //    }
        //}

        var results = Repository.InsertOrUpdateGraph(company, true);
        if (results > 0) return await GetCompany<CompanyDetails>(organizationId, personId, companyId);

        return Result.Failed();
    }

    public async Task<Result> DeleteCompany(int organizationId, int personId, int companyId)
    {
        Logger.LogInformation(
            GetLogMessage("OrganizationId: {@organizationId}, PersonId: {@personId}, CompanyId: {companyId}"),
            organizationId, personId, companyId);

        var company = await Companies
            .Include(x => x.Positions)
            .ThenInclude(x => x.Highlights)
            .Include(x => x.Projects)
            .Where(x => x.OrganizationId == organizationId && x.PersonId == personId && x.Id == companyId)
            .FirstOrDefaultAsync();

        if (company == null) return Result.Failed();

        foreach (var position in company.Positions)
        {
            _positionRepo.Delete(position);
            foreach (var highlight in position.Highlights)
                _highlightRepo.Delete(highlight);
        }

        foreach (var project in company.Projects) _projectRepo.Delete(project);

        Repository.Delete(company);

        var results = UnitOfWork.SaveChanges();
        if (results > 0) return Result.Success();

        return Result.Failed();
    }

    private async Task<int> GetNextJobId(int organizationId)
    {
        var id = await Companies.AsNoTracking()
            .IgnoreQueryFilters()
            .Where(x => x.OrganizationId == organizationId)
            .OrderByDescending(x => x.Id)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
        return id + 1;
    }


    private async Task<int> GetNextPositionId(int organizationId)
    {
        var id = await Positions.AsNoTracking()
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