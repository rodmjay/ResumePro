using System.Diagnostics.CodeAnalysis;
using Bespoke.Core.Builders;
using Bespoke.Data.Enums;
using Bespoke.Data.Extensions;
using Bespoke.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using ResumePro.Services.Helpers;
using ResumePro.Services.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Services.Implementations;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class PositionService : BaseService<Position>, IPositionService
{
    private readonly IRepositoryAsync<Company> _companyRepo;
    private readonly IRepositoryAsync<Highlight> _highlightRepo;
    private readonly IRepositoryAsync<ProjectHighlight> _projectHighlightRepo;

    public PositionService(IServiceProvider serviceProvider,
        IRepositoryAsync<Highlight> highlightRepo,
        IRepositoryAsync<ProjectHighlight> projectHighlightRepo,
        IRepositoryAsync<Company> companyRepo) : base(serviceProvider)
    {
        _highlightRepo = highlightRepo;
        _projectHighlightRepo = projectHighlightRepo;
        _companyRepo = companyRepo;
    }

    private IQueryable<Position> Positions => Repository.Queryable();
    private IQueryable<Company> Companies => _companyRepo.Queryable();
    private IQueryable<Highlight> Highlights => _highlightRepo.Queryable();
    private IQueryable<ProjectHighlight> ProjectHighlights => _projectHighlightRepo.Queryable();

    public async Task<T> GetPosition<T>(int organizationId, int personId, int companyId, int positionId)
        where T : PositionDto
    {
        return await Positions.AsNoTracking().Where(x =>
                x.OrganizationId == organizationId && x.CompanyId == companyId && x.Id == positionId &&
                x.PersonId == personId)
            .ProjectTo<T>(Mapper).FirstAsync();
    }

    public async Task<List<T>> GetPositions<T>(int organizationId, int personId, int companyId) where T : PositionDto
    {
        return await Positions.AsNoTracking().Where(x =>
                x.OrganizationId == organizationId && x.CompanyId == companyId &&
                x.PersonId == personId)
            .ProjectTo<T>(Mapper).ToListAsync();
    }

    public async Task<OneOf<PositionDetails, Result>> CreatePosition(int organizationId, int personId, int companyId,
        PositionOptions options)
    {
        var nextPositionId = await GetNextPositionId(organizationId, personId, companyId);

        var position = new Position
        {
            Id = nextPositionId,
            PersonId = personId,
            OrganizationId = organizationId,
            CompanyId = companyId,
            EndDate = options.EndDate,
            StartDate = options.StartDate,
            JobTitle = options.JobTitle,
            ObjectState = ObjectState.Added
        };

        for (var index = 0; index < options.Highlights.Count; index++)
        {
            var option = options.Highlights[index];
            var highlight = new Highlight
            {
                OrganizationId = organizationId,
                Id = index + 1,
                ObjectState = ObjectState.Added,
                Order = index + 1,
                Text = option.Text
            };

            position.Highlights.Add(highlight);
        }

        for (var index = 0; index < options.Projects.Count; index++)
        {
            var option = options.Projects[index];
            var project = new Project
            {
                OrganizationId = organizationId,
                Id = index + 1,
                PositionId = nextPositionId,
                PersonId = personId,
                CompanyId = companyId,
                ObjectState = ObjectState.Added,
                Description = option.Description,
                Order = index + 1,
                Name = option.Name
            };

            for (var j = 0; j < option.Highlights.Count; j++)
            {
                var highlightOption = option.Highlights[j];

                var highlight = new ProjectHighlight
                {
                    OrganizationId = organizationId,
                    Id = j + 1,
                    ObjectState = ObjectState.Added,
                    Order = j + 1,
                    Text = highlightOption.Text
                };

                project.Highlights.Add(highlight);
            }

            position.Projects.Add(project);
        }

        var records = Repository.InsertOrUpdateGraph(position, true);

        if (records > 0) return await GetPosition<PositionDetails>(organizationId, personId, companyId, position.Id);

        return Result.Failed();
    }

    public async Task<OneOf<PositionDetails, Result>> UpdatePosition(int organizationId, int personId, int companyId,
        int positionId, PositionOptions options)
    {
        var position = await Positions
            .Include(x => x.Projects)
            .ThenInclude(x => x.Highlights)
            .Include(x => x.Highlights)
            .Where(PositionHelpers.GetPredicate(organizationId, personId, companyId, positionId))
            .FirstAsync();

        foreach (var project in position.Projects)
        {
            project.ObjectState = ObjectState.Deleted;

            foreach (var highlight in project.Highlights) highlight.ObjectState = ObjectState.Deleted;
        }

        foreach (var highlight in position.Highlights) highlight.ObjectState = ObjectState.Deleted;

        position.JobTitle = options.JobTitle;
        position.StartDate = options.StartDate;
        position.EndDate = options.EndDate;
        position.ObjectState = ObjectState.Modified;


        foreach (var projectOptions in options.Projects)
        {
            var project = position.Projects.FirstOrDefault(x => x.Id == projectOptions.Id);
            if (project == null)
            {
                project = new Project
                {
                    ObjectState = ObjectState.Added,
                    Order = position.Projects.Count + 1
                };

                position.Projects.Add(project);
            }
            else
            {
                project.ObjectState = ObjectState.Modified;
            }

            project.Name = projectOptions.Name;
            project.Description = projectOptions.Description;
            project.Budget = projectOptions.Budget;

            for (var index = 0; index < projectOptions.Highlights.Count; index++)
            {
                var highlightOptions = projectOptions.Highlights[index];
                var highlight = project.Highlights.FirstOrDefault(x => x.Id == highlightOptions.Id);
                if (highlight == null)
                {
                    highlight = new ProjectHighlight
                    {
                        ObjectState = ObjectState.Added
                    };

                    project.Highlights.Add(highlight);
                }
                else
                {
                    highlight.ObjectState = ObjectState.Modified;
                }

                highlight.Text = highlightOptions.Text;
                highlight.Order = index + 1;
            }
        }


        var records = await Repository.UpdateAsync(position, true);

        if (records > 0)
            return Result.Success();

        return Result.Failed();
    }

    public async Task<Result> DeletePosition(int organizationId, int personId, int companyId, int positionId)
    {
        var success = await Repository
            .DeleteAsync(PositionHelpers.GetPredicate(organizationId, personId, companyId, positionId));

        return success ? Result.Success(positionId) : Result.Failed();
    }

    private async Task<int> GetNextPositionId(int organizationId, int personId, int companyId)
    {
        var id = await Positions.OrderByDescending(x => x.Id)
            .AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.PersonId == personId && x.CompanyId == companyId)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();

        return id + 1;
    }

    private async Task<int> GetNextHighlightId(int organizationId, int personId, int companyId, int positionId)
    {
        var predicate = PredicateBuilder.True<Highlight>()
            .And(x => x.OrganizationId == organizationId)
            .And(x => x.PersonId == personId)
            .And(x => x.CompanyId == companyId)
            .And(x => x.PositionId == positionId);

        var id = await Highlights.AsNoTracking()
            .IgnoreQueryFilters()
            .Where(predicate)
            .OrderByDescending(x => x.Id)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
        return id + 1;
    }

    private async Task<int> GetNextProjectHighlightId(int organizationId, int personId, int companyId, int positionId,
        int projectId)
    {
        var predicate = PredicateBuilder.True<ProjectHighlight>()
            .And(x => x.OrganizationId == organizationId)
            .And(x => x.PersonId == personId)
            .And(x => x.CompanyId == companyId)
            .And(x => x.PositionId == positionId)
            .And(x => x.ProjectId == projectId);

        var id = await ProjectHighlights.AsNoTracking()
            .IgnoreQueryFilters()
            .Where(predicate)
            .OrderByDescending(x => x.Id)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
        return id + 1;
    }
}