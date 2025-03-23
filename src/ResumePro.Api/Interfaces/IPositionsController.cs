namespace ResumePro.Api.Interfaces;

public interface IPositionsController
{
    Task<ActionResult<CompanyDetails>> CreatePosition(int personId, int companyId,
        PositionOptions options);

    Task<ActionResult<CompanyDetails>> UpdatePosition(int personId, int companyId, int positionId,
        PositionOptions options);

    Task<Result> DeletePosition(int personId, int companyId,
        int positionId);

    Task<List<PositionDetails>> GetPositions(int personId, int companyId);
    Task<PositionDetails> GetPosition(int personId, int companyId, int positionId);
}