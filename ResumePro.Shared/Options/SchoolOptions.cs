#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.ComponentModel.DataAnnotations;

namespace ResumePro.Shared.Options;

public class SchoolOptions : ILocation, IName, IEndDate
{
    public int? Id { get; set; }
    public DateTime? EndDate { get; set; }

    [Required] public DateTime? StartDate { get; set; }

    [Required] public string Name { get; set; }

    public List<DegreeOptions> DegreeOptions { get; set; } = new();
    public string Location { get; set; }
}