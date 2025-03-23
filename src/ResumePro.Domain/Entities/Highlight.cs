using Bespoke.Data.Enums;
using Bespoke.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumePro.Domain.Entities;

public sealed class Highlight : BaseEntity<Highlight>, IHighlight
{
    public int OrganizationId { get; set; }
    public Persona Person { get; set; } = null!;
    public int PersonId { get; set; }

    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;

    public Position Position { get; set; } = null!;
    public int PositionId { get; set; }

    public int Id { get; set; }
    public int Order { get; set; }
    public string Text { get; set; } = null!;

    public override void Configure(EntityTypeBuilder<Highlight> builder)
    {
        builder.HasKey(x => new { x.OrganizationId, x.PersonId, x.CompanyId, x.PositionId, x.Id });

        builder.Property(x => x.Text)
            .ConfigureColumn(StringColumnSize.Medium);

        builder.HasOne(x => x.Position)
            .WithMany(x => x.Highlights)
            .HasForeignKey(x => new { x.OrganizationId, x.PersonId, x.CompanyId, x.PositionId })
            .HasPrincipalKey(x => new { x.OrganizationId, x.PersonId, x.CompanyId, x.Id })
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasOne(x => x.Company)
            .WithMany(x => x.Highlights)
            .HasForeignKey(x => new { x.OrganizationId, x.PersonId, x.CompanyId })
            .HasPrincipalKey(x => new { x.OrganizationId, x.PersonId, x.Id })
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasOne(x => x.Person)
            .WithMany(x => x.Highlights)
            .HasForeignKey(x => new { x.OrganizationId, x.PersonId })
            .HasPrincipalKey(x => new { x.OrganizationId, x.Id })
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
    }
}