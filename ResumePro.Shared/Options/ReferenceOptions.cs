#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.ComponentModel.DataAnnotations;

namespace ResumePro.Shared.Options;

public class ReferenceOptions
{
    [Required] public string Text { get; set; }

    [Required] public string Name { get; set; }

    public int Order { get; set; }
}