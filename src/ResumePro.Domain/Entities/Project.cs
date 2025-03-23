using Bespoke.Data.Enums;
using Bespoke.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumePro.Domain.Entities;

public sealed class Project : BaseEntity<Project>, IProject
{
    public int OrganizationId { get; set; }
    public ICollection<ProjectHighlight> Highlights { get; set; } = new List<ProjectHighlight>();

    public Company Company { get; set; } = null!;

    public int PositionId { get; set; }
    public Position Position { get; set; } = null!;

    public int PersonId { get; set; }
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public int Order { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal? Budget { get; set; }

    public override void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(x => new { x.OrganizationId, x.PersonId, x.CompanyId, x.PositionId, x.Id });

        builder.Property(x => x.Name)
            .ConfigureColumn(StringColumnSize.Small);

        builder.Property(x => x.Description)
            .ConfigureColumn(StringColumnSize.Medium, false);

        builder.HasOne(x => x.Company)
            .WithMany(x => x.Projects)
            .HasForeignKey(x => new { x.OrganizationId, x.PersonId, x.CompanyId })
            .HasPrincipalKey(x => new { x.OrganizationId, x.PersonId, x.Id })
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Position)
            .WithMany(x => x.Projects)
            .HasForeignKey(x => new { x.OrganizationId, x.PersonId, x.CompanyId, x.PositionId })
            .HasPrincipalKey(x => new { x.OrganizationId, x.PersonId, x.CompanyId, x.Id })
            .OnDelete(DeleteBehavior.Cascade);
    }
}