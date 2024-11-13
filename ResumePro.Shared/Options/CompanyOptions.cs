#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.ComponentModel.DataAnnotations;

namespace ResumePro.Shared.Options;

public class CompanyOptions
{
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    [MaxLength(255)]
    [Required] public string Company { get; set; }

    [MaxLength(1024)]
    public string Description { get; set; }

    [MaxLength(255)]
    public string Location { get; set; }

    public List<PositionOptions> Positions { get; set; } = new();

    public List<int> JobSkillIds { get; set; } = new();
}