#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Models;

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class CompanyDto : ICompany
{
    public string DisplayEndDate
    {
        get
        {
            if (EndDate == null) return "Present";

            return EndDate.Value.ToShortDateString();
        }
    }

    public string Duration
    {
        get
        {
            var effectiveEndDate = EndDate ?? DateTime.Now;
            var years = effectiveEndDate.Year - StartDate.Year;
            var months = effectiveEndDate.Month - StartDate.Month;

            if (effectiveEndDate.Day < StartDate.Day) months--;

            if (months < 0)
            {
                years--;
                months += 12;
            }

            // Building the duration string
            var duration = "";
            if (years > 0) duration += $"{years} year" + (years > 1 ? "s" : "");
            if (months > 0)
            {
                if (!string.IsNullOrEmpty(duration))
                    duration += ", ";
                duration += $"{months} month" + (months > 1 ? "s" : "");
            }

            return duration;
        }
    }

    public string JobTitle { get; set; } = null!;

    public int Id { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string CompanyName { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string Description { get; set; } = null!;
}