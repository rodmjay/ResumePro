#region Header

// /*

// Author: Rod Johnson, Architect, rodmjay@gmail.com
// */

#endregion

using ResumePro.Geography.Models;

namespace ResumePro.Geography.Interfaces
{
    public interface IStateProvinceStore
    {
        Task<List<T>> GetStateProvincesForCountry<T>(string id) where T : StateProvinceOutput;
    }
}