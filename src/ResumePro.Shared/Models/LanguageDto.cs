namespace ResumePro.Shared.Models;

public class LanguageDto : ILanguage
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code2 { get; set; } = null!;
    public string Code3 { get; set; } = null!;
}
