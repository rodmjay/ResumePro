#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.Extensions.Logging;
using ResumePro.Extensions;
using ResumePro.Shared.Models;

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class CompanyService(
    IServiceProvider serviceProvider)
    : BaseService<Company>(serviceProvider), ICompanyService
{
    private IQueryable<Company> Companies => UnitOfWork.RepositoryAsync<Company>().Queryable();
    private IQueryable<Resume> Resumes => UnitOfWork.RepositoryAsync<Resume>().Queryable();
    private IQueryable<Highlight> Highlights => UnitOfWork.RepositoryAsync<Highlight>().Queryable();
    private IQueryable<Project> Projects => UnitOfWork.RepositoryAsync<Project>().Queryable();
    private IQueryable<PersonaSkill> PersonSkills => UnitOfWork.RepositoryAsync<PersonaSkill>().Queryable();
    private IQueryable<Position> Positions => UnitOfWork.RepositoryAsync<Position>().Queryable();

    public Task<List<T>> GetCompanies<T>(int organizationId, int personId) where T : CompanyDto
    {
        return Companies
            .Where(x => x.OrganizationId == organizationId && x.PersonId == personId)
            .ProjectTo<T>(Mapper)
            .ToListAsync();
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

        var personSkills = await PersonSkills
            .Where(x => x.OrganizationId == organizationId && x.PersonId == personId)
            .Select(x => x.SkillId).ToListAsync();

        var company = options
            .Create(personSkills, organizationId, personId, await GetNextCompanyId(organizationId));

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

                UnitOfWork.RepositoryAsync<Resume>().InsertOrUpdateGraph(resume);
            }

            UnitOfWork.RepositoryAsync<Resume>().Commit();

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
            .Include(x => x.Positions)
            .ThenInclude(x => x.Projects)
            .ThenInclude(x => x.Highlights)
            .Include(x => x.Skills)
            .Where(x => x.OrganizationId == organizationId && x.PersonId == personId && x.Id == companyId)
            .FirstOrDefaultAsync();

        if (company == null)
            return Result.Failed();

        company.Update(options);

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
            .Include(x => x.Positions)
            .ThenInclude(x => x.Projects)
            .Where(x => x.OrganizationId == organizationId && x.PersonId == personId && x.Id == companyId)
            .FirstOrDefaultAsync();

        if (company == null) return Result.Failed();

        Repository.Delete(company);

        var results = UnitOfWork.SaveChanges();
        if (results > 0) return Result.Success();

        return Result.Failed();
    }

    private async Task<int> GetNextCompanyId(int organizationId)
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