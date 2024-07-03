#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared;

public interface IPersona
{
    int Id { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string Email { get; set; }
    string PhoneNumber { get; set; }
    string LinkedIn { get; set; }
    string GitHub { get; set; }
    string City { get; set; }
    string State { get; set; }
}