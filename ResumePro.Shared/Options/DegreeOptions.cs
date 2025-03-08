#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.ComponentModel.DataAnnotations;

namespace ResumePro.Shared.Options;

public class DegreeOptions : IName, IOrder
{
    public int? Id { get; set; }

    [MaxLength(255)]
    [Required] public string Name { get; set; }

    public int Order { get; set; }
}