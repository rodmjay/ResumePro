using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Shared.Enums;

namespace ResumePro.Domain.Entities;

public sealed class PersonaLanguage : BaseEntity<PersonaLanguage>, IPersonaLanguage
{
    public Persona Persona { get; set; } = null!;
    public Language Language { get; set; } = null!;
    public int OrganizationId { get; set; }
    public int PersonId { get; set; }
    public string Code3 { get; set; } = null!;

    public LanguageLevel Proficiency { get; set; }

    public override void Configure(EntityTypeBuilder<PersonaLanguage> builder)
    {
        builder.HasKey(x => new { x.OrganizationId, x.PersonId, x.Code3 });

        builder.HasOne(x => x.Persona)
            .WithMany(x => x.Languages)
            .HasForeignKey(x => new { x.OrganizationId, x.PersonId })
            .HasPrincipalKey(x => new { x.OrganizationId, x.Id })
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Language)
            .WithMany(x => x.People)
            .HasForeignKey(x => x.Code3)
            .HasPrincipalKey(x => x.Code3);
    }
}