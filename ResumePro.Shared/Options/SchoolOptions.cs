#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Options;

public class SchoolOptions
{
    public int? Id { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime StartDate { get; set; }
    public string Name { get; set; }

    public List<DegreeOptions> Degrees { get; set; } = new List<DegreeOptions>();
}