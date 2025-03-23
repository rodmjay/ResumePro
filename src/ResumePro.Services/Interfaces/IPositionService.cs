using Bespoke.Services.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Services.Interfaces;

public interface IPositionService : IService<Position>
{
    Task<T> GetPosition<T>(int organizationId, int personId, int companyId, int positionId) where T : PositionDto;
    Task<List<T>> GetPositions<T>(int organizationId, int personId, int companyId) where T : PositionDto;

    Task<OneOf<PositionDetails, Result>> CreatePosition(int organizationId, int personId, int companyId,
        PositionOptions options);

    Task<OneOf<PositionDetails, Result>> UpdatePosition(int organizationId, int personId, int companyId, int positionId,
        PositionOptions options);

    Task<Result> DeletePosition(int organizationId, int personId, int companyId, int positionId);
}