#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.ComponentModel.DataAnnotations;

namespace ResumePro.Shared.Options;

public class HighlightOptions
{
    public int? Id { get; set; }

    [MaxLength(512)]
    [Required] public string Text { get; set; }

    public int? Order { get; set; }
}