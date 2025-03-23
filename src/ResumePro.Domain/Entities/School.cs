using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumePro.Domain.Entities;

public sealed class School : BaseEntity<School>, ISchool
{
    public Persona Persona { get; set; } = null!;
    public ICollection<Degree> Degrees { get; set; } = new List<Degree>();
    public int OrganizationId { get; set; }
    public int Id { get; set; }
    public int PersonId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Name { get; set; } = null!;
    public string Location { get; set; } = null!;

    public override void Configure(EntityTypeBuilder<School> builder)
    {
        builder.HasKey(x => new { x.OrganizationId, x.Id });

        builder.HasOne(x => x.Persona)
            .WithMany(x => x.Schools)
            .HasForeignKey(x => new { x.OrganizationId, x.PersonId })
            .HasPrincipalKey(x => new { x.OrganizationId, x.Id })
            .OnDelete(DeleteBehavior.Cascade);
    }
}