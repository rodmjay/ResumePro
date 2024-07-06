#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Interfaces;

namespace ResumePro.Shared;

public class CompanyDto : ICompany
{
    public int OrganizationId { get; set; }
    public string Name { get; set; }
    public string Headquarters { get; set; }
    public string Description { get; set; }
    public int Id { get; set; }
}