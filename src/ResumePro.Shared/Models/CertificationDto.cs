namespace ResumePro.Shared.Models;

public class CertificationDto : ICertification
{
    [JsonIgnore] public int OrganizationId { get; set; }

    public string Name { get; set; } = null!;
    public string Body { get; set; } = null!;

    [JsonIgnore] public int PersonId { get; set; }

    public int Id { get; set; }
    public DateTime Date { get; set; }
}