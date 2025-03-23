using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumePro.Domain.Entities;

public class Language : BaseEntity<Language>, ILanguage
{
    public string NativeName { get; set; } = null!;

    public ICollection<PersonaLanguage> People { get; set; } = new List<PersonaLanguage>();
    public string Name { get; set; } = null!;
    public string Code2 { get; set; } = null!;
    public string Code3 { get; set; } = null!;

    public override void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.HasKey(x => x.Code3);
    }
}