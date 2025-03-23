namespace ResumePro.Shared.Models;

public class HighlightDto : IHighlight
{
    public int Id { get; set; }

    public int Order { get; set; }

    public string Text { get; set; } = null!;
}