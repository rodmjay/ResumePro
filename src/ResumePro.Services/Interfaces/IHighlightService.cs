using Bespoke.Services.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Services.Interfaces;

public interface IHighlightService : IService<Highlight>
{
    Task<List<T>> GetHighlights<T>(int organizationId, int companyId, int positionId, int? projectId)
        where T : HighlightDto;

    Task<T> GetHighlight<T>(int organizationId, int companyId, int positionId, int highlightId) where T : HighlightDto;

    Task<OneOf<HighlightDto, Result>> CreateHighlight(int organizationId, int personId, int companyId, int positionId,
        int? projectId,
        HighlightOptions options);

    Task<OneOf<HighlightDto, Result>> UpdateHighlight(int organizationId, int personId, int companyId, int positionId,
        int highlightId,
        HighlightOptions options);

    Task<Result> DeleteHighlight(int organizationId, int personId, int companyId, int positionId, int? projectId,
        int highlightId);
}