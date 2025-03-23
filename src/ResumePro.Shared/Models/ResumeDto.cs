namespace ResumePro.Shared.Models;

public class ResumeDto : IResume
{
    public ResumeSettingsDto Settings { get; set; } = null!;

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string LinkedIn { get; set; } = null!;
    public string GitHub { get; set; } = null!;
    public string City { get; set; } = null!;
    public string State { get; set; } = null!;
    public string Country { get; set; } = null!;
    public int JobCount { get; set; }
    public int SkillCount { get; set; }

    [JsonIgnore] public int PersonId { get; set; }

    public int Id { get; set; }

    public string JobTitle { get; set; } = null!;
    public string Description { get; set; } = null!;

    [JsonIgnore] public int OrganizationId { get; set; }
}