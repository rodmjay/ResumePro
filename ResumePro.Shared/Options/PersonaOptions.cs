#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ResumePro.Shared.Options;

public class PersonaOptions
{
    [Required] public string City { get; set; }

    public int StateId { get; set; }

    [Required] public string FirstName { get; set; }

    [Required] public string LastName { get; set; }

    [Required] public string Email { get; set; }

    public string GitHub { get; set; }
    public string LinkedIn { get; set; }
}