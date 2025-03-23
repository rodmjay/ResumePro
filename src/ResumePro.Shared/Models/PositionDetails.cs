namespace ResumePro.Shared.Models;

public sealed class PositionDetails : PositionDto
{
    public List<HighlightDto> Highlights { get; set; } = new();
    public List<ProjectDetails> Projects { get; set; } = new();
}