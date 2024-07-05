#region Header

// /*

// Author: Rod Johnson, Architect, rodmjay@gmail.com
// */

#endregion

using ResumePro.Shared.Common;

namespace ResumePro.Geography.Models
{
    public class GeographyErrorDescriber
    {
        public virtual Error EnableCountryError()
        {
            return new()
            {
                Code = nameof(EnableCountryError),
                Description = "Unable to enable country"
            };
        }

        public virtual Error DisableCountryError()
        {
            return new()
            {
                Code = nameof(DisableCountryError),
                Description = "Unable to disable country"
            };
        }

        public virtual Error CountryAlreadyEnabled()
        {
            return new()
            {
                Code = nameof(CountryAlreadyEnabled),
                Description = "country already enabled"
            };
        }

        public virtual Error CountryAlreadyDisabled()
        {
            return new()
            {
                Code = nameof(CountryAlreadyDisabled),
                Description = "country already disabled"
            };
        }
    }
}