#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.ComponentModel.DataAnnotations;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Shared.Options;

public class ProjectOptions : IBudget, IDescription, IName, IOrder
{
    public int? Id { get; set; }
    public decimal? Budget { get; set; } = 0;

    [MaxLength(512)]
    public string Description { get; set; }

    [MaxLength(255)]
    [Required] public string Name { get; set; }

    public int Order { get; set; }
    public List<HighlightOptions> Highlights { get; set; } = new();
}