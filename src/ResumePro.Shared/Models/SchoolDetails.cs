namespace ResumePro.Shared.Models;

public class SchoolDetails : SchoolDto
{
    public List<DegreeDto> Degrees { get; set; } = new();
}