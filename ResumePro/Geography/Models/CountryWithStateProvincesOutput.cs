#region Header

// /*

// Author: Rod Johnson, Architect, rodmjay@gmail.com
// */

#endregion

namespace ResumePro.Geography.Models
{
    public class CountryWithStateProvincesOutput : CountryDetails
    {
        public CountryWithStateProvincesOutput()
        {
            StateProvinces = new List<StateProvinceOutput>();
        }

        public List<StateProvinceOutput> StateProvinces { get; set; }
    }
}