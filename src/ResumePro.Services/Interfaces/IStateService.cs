using ResumePro.Shared.Models;

namespace ResumePro.Services.Interfaces;

public interface IStateService
{
    Task<List<StateProvinceOutput>> GetStatesDropdown(string countryId);
}