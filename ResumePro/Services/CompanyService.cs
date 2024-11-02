#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.Extensions.Logging;
using ResumePro.Shared.Models;

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class CompanyService(
    IServiceProvider serviceProvider,
    IRepositoryAsync<Project> projectRepo,
    IRepositoryAsync<Resume> resumeRepo,
    IRepositoryAsync<PersonaSkill> personSkillRepo,
    IRepositoryAsync<Position> positionRepo,
    IRepositoryAsync<Highlight> highlightRepo)
    : BaseService<Company>(serviceProvider), ICompanyService
{
    private IQueryable<Company> Companies => Repository.Queryable();
    private IQueryable<Resume> Resumes => resumeRepo.Queryable();
    private IQueryable<Highlight> Highlights => highlightRepo.Queryable();
    private IQueryable<Project> Projects => projectRepo.Queryable();
    private IQueryable<PersonaSkill> PersonSkills => personSkillRepo.Queryable();
    private IQueryable<Position> Positions => positionRepo.Queryable();

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
            .ProjectTo<T>(Mapper).FirstOrDefaultAsync();
    }

    public async Task<OneOf<CompanyDetails, Result>> CreateCompany(int organizationId, int personId, CompanyOptions options)
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


        var nextHighlightId = await GetNextHighlightId(organizationId);
        var nextProjectId = await GetNextProjectId(organizationId);

        foreach (var availableSkill in availableSkills)
            company.Skills.Add(new CompanySkill
            {
                ObjectState = ObjectState.Added,
                SkillId = availableSkill,
                PersonId = personId,
                OrganizationId = organizationId
            });

        //for (var index = 0; index < options.ProjectOptions.Count; index++)
        //{
        //    var project = options.ProjectOptions[index];
        //    var projectEntity = new Project
        //    {
        //        OrganizationId = organizationId,
        //        Budget = project.Budget,
        //        Description = project.Description,
        //        Id = nextProjectId++,
        //        Name = project.Name,
        //        Order = index + 1,
        //        ObjectState = ObjectState.Added
        //    };

        //    for (var i = 0; i < project.HighlightOptions.Count; i++)
        //    {
        //        var highlight = project.HighlightOptions[i];
        //        projectEntity.Highlights.Add(new ProjectHighlight()
        //        {
        //            OrganizationId = organizationId,
        //            ObjectState = ObjectState.Added,
        //            Id = nextHighlightId++,
        //            Text = highlight.Text,
        //            Order = i + 1
        //        });
        //    }

        //    company.Projects.Add(projectEntity);
        //}

        for (int i = 0; i < options.PositionOptions.Count; i++)
        {
            var position = options.PositionOptions[i];

            var ent = new Position()
            {
                StartDate = position.StartDate,
                EndDate = position.EndDate,
                ObjectState = ObjectState.Added,
            };

            for (var index = 0; index < position.HighlightOptions.Count; index++)
            {
                var highlight = position.HighlightOptions[index];
                ent.Highlights.Add(new Highlight()
                {
                    Id = nextHighlightId++,
                    ObjectState = ObjectState.Added,
                    Order = index,
                    Text = highlight.Text,
                    OrganizationId = organizationId
                });
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

                resumeRepo.InsertOrUpdateGraph(resume);
            }

            resumeRepo.Commit();

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

        for (var i = 0; i < options.PositionOptions.Count; i++)
        {
            var positionOptions = options.PositionOptions[i];
            // todo: add stuff here
            var position = company.Positions.FirstOrDefault(x => x.Id == positionOptions.Id);
            if (position == null)
            {
                position = new Position()
                {
                    ObjectState = ObjectState.Added,
                    CompanyId = companyId,
                    PersonId = personId,
                    Id = nextPositionId++,
                };
            }
            else
            {
                position.ObjectState = ObjectState.Modified;
            }

            position.StartDate = positionOptions.StartDate;
            position.EndDate = positionOptions.EndDate;
            position.JobTitle = positionOptions.JobTitle;

            for (var index = 0; index < positionOptions.HighlightOptions.Count; index++)
            {
                var highlight = positionOptions.HighlightOptions[index];

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
        Logger.LogInformation(GetLogMessage("OrganizationId: {@organizationId}, PersonId: {@personId}, CompanyId: {companyId}"),
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
            positionRepo.Delete(position);
            foreach (var highlight in position.Highlights)
                highlightRepo.Delete(highlight);
        }

        foreach (var project in company.Projects) projectRepo.Delete(project);

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