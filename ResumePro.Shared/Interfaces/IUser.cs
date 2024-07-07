#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Interfaces;

public interface IUser
{
    int Id { get; set; }
    string UserName { get; set; }
    string Email { get; set; }
    string PhoneNumber { get; set; }
}