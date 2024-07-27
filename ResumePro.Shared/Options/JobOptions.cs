#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.ComponentModel.DataAnnotations;

namespace ResumePro.Shared.Options;

public class JobOptions
{
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    [MaxLength(255)]
    [Required] public string Company { get; set; }

    [MaxLength(1024)]
    public string Description { get; set; }

    [MaxLength(255)]
    public string Location { get; set; }
    
    [MaxLength(255)]
    [Required] public string JobTitle { get; set; }

    public List<HighlightOptions> HighlightOptions { get; set; } = new();
    public List<ProjectOptions> ProjectOptions { get; set; } = new();

    public List<int> JobSkillIds { get; set; } = new();
}