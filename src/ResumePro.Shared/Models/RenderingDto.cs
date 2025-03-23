namespace ResumePro.Shared.Models;

public class RenderingDto : IRendering
{
    public string Format { get; set; } = null!;
    public string Engine { get; set; } = null!;
    public int OrganizationId { get; set; }
    public int ResumeId { get; set; }
    public int TemplateId { get; set; }
    public DateTime RenderDate { get; set; }
    public string Text { get; set; } = null!;
}