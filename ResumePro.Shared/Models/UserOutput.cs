#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Models;

public class UserOutput : IUser
{
    public string OrganizationName { get; set; }
    public int OrganizationId { get; set; }
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}