namespace ResumePro.Shared.Options;

public class TextResumeOptions
{
    public string Description { get; set; } = null!;

    public List<TextHighlightOptions> Highlights { get; set; } = new();
}