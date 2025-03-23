using Bespoke.Data.Enums;
using Bespoke.Data.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumePro.Domain.Entities;

public sealed class Position : BaseEntity<Position>, IPosition
{
    public int OrganizationId { get; set; }
    public int PersonId { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;
    public ICollection<Project> Projects { get; set; } = new List<Project>();

    public ICollection<Highlight> Highlights { get; set; } = new List<Highlight>();
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string JobTitle { get; set; } = null!;

    public override void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.HasKey(x => new { x.OrganizationId, x.PersonId, x.CompanyId, x.Id });

        builder.Property(x => x.JobTitle)
            .ConfigureColumn(StringColumnSize.Small);

        builder.HasOne(x => x.Company)
            .WithMany(x => x.Positions)
            .HasForeignKey(x => new { x.OrganizationId, x.PersonId, x.CompanyId })
            .HasPrincipalKey(x => new { x.OrganizationId, x.PersonId, x.Id });
    }
}