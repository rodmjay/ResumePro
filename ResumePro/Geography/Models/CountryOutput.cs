﻿#region Header

// /*

// Author: Rod Johnson, Architect, rodmjay@gmail.com
// */

#endregion

using ResumePro.Geography.Interfaces;

namespace ResumePro.Geography.Models
{
    public class CountryOutput : ICountry
    {
        public string Name { get; set; }

        public string Iso2 { get; set; }

        public string CapsName { get; set; }

        public string Iso3 { get; set; }

        public int? NumberCode { get; set; }

        public int PhoneCode { get; set; }
    }
}