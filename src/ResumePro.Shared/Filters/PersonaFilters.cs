namespace ResumePro.Shared.Filters;

public class PersonaFilters
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public List<int> Skills { get; set; } = new();
    public List<int> States { get; set; } = new();
}
