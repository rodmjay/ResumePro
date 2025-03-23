namespace ResumePro.Shared.Models;

public class TemplateDto : ITemplate
{
    public bool IsGlobal { get; set; }
    public string Name { get; set; } = null!;
    public string Source { get; set; } = null!;
    public string Format { get; set; } = null!;
    public string Engine { get; set; } = null!;
    public int Id { get; set; }
}