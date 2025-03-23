namespace ResumePro.Api.Interfaces;

public interface IProjectHighlightsController
{
    Task<HighlightDto> GetHighlight(int personId,
        int companyId,
        int positionId,
        int projectId,
        int highlightId);

    Task<List<HighlightDto>> GetHighlights(int personId,
        int companyId,
        int positionId,
        int projectId);

    Task<ActionResult<HighlightDto>> CreateHighlight(int personId,
        int companyId,
        int positionId,
        int projectId,
        HighlightOptions options);

    Task<ActionResult<HighlightDto>> UpdateHighlight(int personId,
        int companyId,
        int positionId,
        int projectId,
        int highlightId,
        HighlightOptions options);

    Task<Result> DeleteHighlight(int personId,
        int companyId,
        int positionId,
        int projectId,
        int highlightId);
}