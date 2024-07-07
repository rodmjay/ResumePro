#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Interfaces;

namespace ResumePro.Shared;

public class HiringManagerDto : IHiringManager
{
    public virtual int OrganizationId { get; set; }
    public virtual int Id { get; set; }
    public virtual string FirstName { get; set; }
    public virtual string LastName { get; set; }
    public virtual string Email { get; set; }
    public virtual string Phone { get; set; }
    public virtual string Department { get; set; }
    public virtual int CompanyId { get; set; }
}