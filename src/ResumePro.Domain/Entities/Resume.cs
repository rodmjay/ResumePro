using Bespoke.Data.Enums;
using Bespoke.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumePro.Domain.Entities;

public sealed class Resume : BaseEntity<Resume>, IResume
{
    public Persona Persona { get; set; } = null!;
    public ICollection<ResumeCompany> Companies { get; set; } = new List<ResumeCompany>();
    public ICollection<ResumeSkill> Skills { get; set; } = new List<ResumeSkill>();
    public ICollection<Rendering> Renderings { get; set; } = new List<Rendering>();
    public ResumeSettings ResumeSettings { get; set; } = null!;
    public int OrganizationId { get; set; }
    public int PersonId { get; set; }
    public int Id { get; set; }
    public string JobTitle { get; set; } = null!;
    public string Description { get; set; } = null!;

    public override void Configure(EntityTypeBuilder<Resume> builder)
    {
        builder.HasKey(x => new { x.OrganizationId, x.PersonId, x.Id });

        builder.Property(x => x.JobTitle)
            .ConfigureColumn(StringColumnSize.Small);

        builder.Property(x => x.Description)
            .ConfigureColumn(StringColumnSize.Large);

        builder.HasOne(x => x.Persona)
            .WithMany(x => x.Resumes)
            .HasForeignKey(x => new { x.OrganizationId, x.PersonId })
            .HasPrincipalKey(x => new { x.OrganizationId, x.Id })
            .OnDelete(DeleteBehavior.Cascade);
    }
}
