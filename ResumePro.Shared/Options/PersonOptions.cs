#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.ComponentModel.DataAnnotations;
using ResumePro.Shared.Interfaces;

namespace ResumePro.Shared.Options;

public class PersonOptions : ICity, IPhoneNumber, IFirstAndLastName, IEmail, IGitHub, ILinkedIn, IStateId
{
    [Required] public string City { get; set; }

    public int StateId { get; set; }

    [MaxLength(64)]
    public string PhoneNumber { get; set; }

    [MaxLength(64)]
    [Required] public string FirstName { get; set; }

    [MaxLength(64)]
    [Required] public string LastName { get; set; }

    [Required]
    [MaxLength(64)]
    public string Email { get; set; }

    [MaxLength(64)]
    public string GitHub { get; set; }

    [MaxLength(64)]
    public string LinkedIn { get; set; }
}