using Bespoke.Data.Enums;
using Bespoke.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumePro.Domain.Entities;

public sealed class Certification : BaseEntity<Certification>, ICertification
{
    public Persona Persona { get; set; } = null!;
    public int OrganizationId { get; set; }
    public string Name { get; set; } = null!;
    public string Body { get; set; } = null!;
    public DateTime Date { get; set; }
    public int PersonId { get; set; }
    public int Id { get; set; }

    public override void Configure(EntityTypeBuilder<Certification> builder)
    {
        builder.HasKey(x => new { x.OrganizationId, x.Id });

        builder.Property(x => x.Name)
            .ConfigureColumn(StringColumnSize.Small);

        builder.Property(x => x.Body)
            .ConfigureColumn(StringColumnSize.Small);

        builder.HasOne(x => x.Persona)
            .WithMany(x => x.Certifications)
            .HasForeignKey(x => new { x.OrganizationId, x.PersonId })
            .HasPrincipalKey(x => new { x.OrganizationId, x.Id })
            .OnDelete(DeleteBehavior.Cascade);
    }
}