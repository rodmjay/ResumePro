#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Models;

public class PersonaDto : IPersona
{
    public string State { get; set; } = null!;
    public int SkillCount { get; set; }
    public int ResumeCount { get; set; }
    public int JobCount { get; set; }
    public int ReferencesCount { get; set; }
    public int CertificationCount { get; set; }
    public string Country { get; set; } = null!;
    public int StateId { get; set; }
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string LinkedIn { get; set; } = null!;
    public string GitHub { get; set; } = null!;
    public string City { get; set; } = null!;
    public int OrganizationId { get; set; }
}