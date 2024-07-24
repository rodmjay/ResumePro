#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.ComponentModel.DataAnnotations;

namespace ResumePro.Shared.Options;

public class JobOptions
{
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    [Required]
    public string Company { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }

    [Required]
    public string Title { get; set; }

    public List<HighlightOptions> HighlightOptions { get; set; } = new List<HighlightOptions>();
    public List<ProjectOptions> ProjectOptions { get; set; } = new List<ProjectOptions>();

    public List<int> JobSkillIds { get; set; } = new List<int>();

}