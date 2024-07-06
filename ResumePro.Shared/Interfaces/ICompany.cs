#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Interfaces;

public interface ICompany
{
    int OrganizationId { get; set; }
    string Name { get; set; }
    string Headquarters { get; set; }
    string Description { get; set; }
    int Id { get; set; }
}