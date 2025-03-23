namespace ResumePro.Shared.Models;

public class PositionDto : IPosition
{
    public string JobTitle { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int Id { get; set; }
}