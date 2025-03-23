using Bespoke.Data.Enums;
using Bespoke.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumePro.Domain.Entities;

public sealed class ProjectHighlight : BaseEntity<ProjectHighlight>
{
    public int OrganizationId { get; set; }
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public Project Project { get; set; } = null!;
    public int ProjectId { get; set; }
    public int PositionId { get; set; }
    public int PersonId { get; set; }
    public int Order { get; set; }
    public string Text { get; set; } = null!;

    public override void Configure(EntityTypeBuilder<ProjectHighlight> builder)
    {
        builder.HasKey(x => new { x.OrganizationId, x.PersonId, x.CompanyId, x.PositionId, x.ProjectId, x.Id });

        builder.Property(x => x.Text)
            .ConfigureColumn(StringColumnSize.Medium);

        builder.HasOne(x => x.Project)
            .WithMany(x => x.Highlights)
            .HasForeignKey(x => new { x.OrganizationId, x.PersonId, x.CompanyId, x.PositionId, x.ProjectId })
            .HasPrincipalKey(x => new { x.OrganizationId, x.PersonId, x.CompanyId, x.PositionId, x.Id })
            .OnDelete(DeleteBehavior.Cascade);
    }
}
