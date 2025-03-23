using System.Diagnostics.CodeAnalysis;
using Bespoke.Data.Enums;
using Bespoke.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResumePro.Services.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Services.Implementations;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class HighlightService : BaseService<Highlight>, IHighlightService
{
    public HighlightService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    private IQueryable<Highlight> Highlights => Repository.Queryable();

    public Task<List<T>> GetHighlights<T>(int organizationId, int companyId, int positionId, int? projectId)
        where T : HighlightDto
    {
        return Highlights
            .AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.PositionId == positionId)
            .OrderBy(x => x.Order)
            .ProjectTo<T>(Mapper)
            .ToListAsync();
    }

    public Task<T> GetHighlight<T>(int organizationId, int companyId, int positionId, int highlightId)
        where T : HighlightDto
    {
        return Highlights
            .AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.Id == highlightId)
            .ProjectTo<T>(Mapper)
            .FirstAsync();
    }

    public async Task<OneOf<HighlightDto, Result>> CreateHighlight(int organizationId, int personId, int companyId,
        int positionId,
        int? projectId, HighlightOptions options)
    {
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, CompanyId: {@companyId}, ProjectId: {@projectId}, Options: {@options}"),
            organizationId, personId, companyId, positionId, projectId, options);

        var lastHighlight = await
            Highlights.Where(x => x.OrganizationId == organizationId && x.PositionId == positionId)
                .AsNoTracking()
                .OrderByDescending(x => x.Order)
                .FirstOrDefaultAsync();

        var highlight = new Highlight
        {
            ObjectState = ObjectState.Added,
            OrganizationId = organizationId,
            Text = options.Text,
            PositionId = positionId,
            Id = await GetNextHighlightId(organizationId)
        };

        if (lastHighlight == null)
            highlight.Order = 1;
        else
            highlight.Order = lastHighlight.Order + 1;

        var results = Repository.InsertOrUpdateGraph(highlight, true);
        if (results > 0)
            return await GetHighlight<HighlightDto>(organizationId, companyId, positionId, highlight.Id);

        return Result.Failed();
    }

    public async Task<OneOf<HighlightDto, Result>> UpdateHighlight(int organizationId, int personId, int companyId,
        int positionId,
        int highlightId, HighlightOptions options)
    {
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, CompanyId: {@companyId}, PositionId: {@positionId}, HighlightId: {@highlightId}, Options: {@options}"),
            organizationId, personId, companyId, positionId, highlightId, options);

        var highlight = await Highlights
            .Where(x => x.OrganizationId == organizationId && x.PositionId == positionId && x.Id == highlightId)
            .FirstOrDefaultAsync();

        if (highlight == null) return Result.Failed();

        var highlights = await Highlights
            .Where(x => x.OrganizationId == organizationId)
            .OrderBy(x => x.Order)
            .ToListAsync();

        highlights.Remove(highlight);

        highlight.Text = options.Text;
        highlight.ObjectState = ObjectState.Modified;

        var index = options.Order.GetValueOrDefault() - 1;

        if (index < 0) index = 0;
        if (index > highlights.Count) index = highlights.Count;

        highlights.Insert(index, highlight);

        for (var i = 0; i < highlights.Count; i++)
        {
            highlights[i].Order = i + 1;
            highlights[i].ObjectState = ObjectState.Modified;

            Repository.InsertOrUpdateGraph(highlights[0]);
        }

        var results = Repository.Commit();
        if (results > 0)
            return await GetHighlight<HighlightDto>(organizationId, companyId, positionId, highlight.Id);

        return Result.Failed();
    }

    public async Task<Result> DeleteHighlight(int organizationId, int personId, int companyId, int positionId,
        int? projectId,
        int highlightId)
    {
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, CompanyId: {@companyId}, Position: {@positionId}, ProjectId: {@projectId}, HighlightId: {@highlightId}"),
            organizationId, personId, companyId, positionId, projectId, highlightId);

        var highlight = await Highlights
            .Where(x => x.OrganizationId == organizationId && x.PositionId == positionId && x.Id == highlightId)
            .FirstOrDefaultAsync();

        if (highlight == null) return Result.Failed();

        var highlights = await Highlights
            .Where(x => x.OrganizationId == organizationId && x.PositionId == positionId)
            .OrderBy(x => x.Order)
            .ToListAsync();

        highlights.Remove(highlight);

        for (var i = 0; i < highlights.Count; i++)
        {
            highlights[i].Order = i + 1;
            highlights[i].ObjectState = ObjectState.Modified;

            Repository.InsertOrUpdateGraph(highlights[i]);
        }

        highlight.ObjectState = ObjectState.Deleted;
        Repository.InsertOrUpdateGraph(highlight);

        var results = Repository.Commit();
        if (results > 0)
            return Result.Success();

        return Result.Failed();
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