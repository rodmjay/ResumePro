#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.ComponentModel.DataAnnotations;

namespace ResumePro.Shared.Options;

public class ReferenceOptions : IText, IName, IOrder
{
    [MaxLength(1024)]
    [Required] public string Text { get; set; }

    [MaxLength(255)]
    [Required] public string Name { get; set; }

    public int Order { get; set; }
}