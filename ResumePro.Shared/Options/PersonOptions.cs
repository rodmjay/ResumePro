#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ResumePro.Shared.Options;

public class PersonOptions
{
    [Required] public string City { get; set; }

    public int StateId { get; set; }

    [Phone]
    public string PhoneNumber { get; set; }

    [Required] public string FirstName { get; set; }

    [Required] public string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Url]
    public string GitHub { get; set; }

    [Url]
    public string LinkedIn { get; set; }
}