#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Models;

public class ResumeDto : IResume
{
    public ResumeSettingsDto Settings { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string LinkedIn { get; set; }
    public string GitHub { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public int JobCount { get; set; }
    public int SkillCount { get; set; }

    [JsonIgnore] public int PersonId { get; set; }

    public int Id { get; set; }

    public string JobTitle { get; set; }
    public string Description { get; set; }

    [JsonIgnore] public int OrganizationId { get; set; }
}