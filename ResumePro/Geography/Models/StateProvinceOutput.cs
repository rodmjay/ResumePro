#region Header

// /*

// Author: Rod Johnson, Architect, rodmjay@gmail.com
// */

#endregion

using ResumePro.Geography.Interfaces;

namespace ResumePro.Geography.Models
{
    public class StateProvinceOutput : IStateProvince
    {
        public string Name { get; set; }

        public string Abbrev { get; set; }

        public string Code { get; set; }
    }
}