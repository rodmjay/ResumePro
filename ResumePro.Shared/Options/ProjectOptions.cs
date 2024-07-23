#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.ComponentModel.DataAnnotations;

namespace ResumePro.Shared.Options;

public class ProjectOptions
{
    public int? Id { get; set; }
    public decimal? Budget { get; set; } = 0;
    public string Description { get; set; }

    [Required]
    public string Name { get; set; }
    public int Order { get; set; }
    public List<HighlightOptions> HighlightOptions { get; set; } = new List<HighlightOptions>();
}