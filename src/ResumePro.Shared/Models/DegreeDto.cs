namespace ResumePro.Shared.Models;

public class DegreeDto : IDegree
{
    [JsonIgnore] public int SchoolId { get; set; }

    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public int Order { get; set; }
}