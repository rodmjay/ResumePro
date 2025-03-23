namespace ResumePro.Api.Interfaces;

public interface IHighlightsController
{
    Task<HighlightDto> GetHighlight(int personId, int companyId, int positionId,
        int highlightId);

    Task<List<HighlightDto>> GetHighlights(int personId, int companyId, int positionId);

    Task<ActionResult<HighlightDto>> CreateHighlight(int personId, int companyId, int positionId,
        HighlightOptions options);

    Task<ActionResult<HighlightDto>> UpdateHighlight(int personId, int companyId, int positionId,
        int highlightId,
        HighlightOptions options);

    Task<Result> DeleteHighlight(int personId, int companyId, int positionId,
        int highlightId);
}