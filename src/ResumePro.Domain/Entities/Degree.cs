using Bespoke.Data.Enums;
using Bespoke.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumePro.Domain.Entities;

public sealed class Degree : BaseEntity<Degree>, IDegree
{
    public int OrganizationId { get; set; }
    public School School { get; set; } = null!;
    public int SchoolId { get; set; }
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Order { get; set; }

    public override void Configure(EntityTypeBuilder<Degree> builder)
    {
        builder.HasKey(x => new { x.OrganizationId, x.Id });

        builder.Property(x => x.Name)
            .ConfigureColumn(StringColumnSize.Small);

        builder.HasOne(x => x.School)
            .WithMany(x => x.Degrees)
            .HasForeignKey(x => new { x.OrganizationId, x.SchoolId })
            .HasPrincipalKey(x => new { x.OrganizationId, x.Id })
            .OnDelete(DeleteBehavior.Cascade);
    }
}