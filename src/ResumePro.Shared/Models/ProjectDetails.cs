namespace ResumePro.Shared.Models;

public class ProjectDetails : ProjectDto
{
    public List<HighlightDto> Highlights { get; set; } = null!;
}