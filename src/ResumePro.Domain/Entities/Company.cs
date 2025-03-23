using Bespoke.Data.Enums;
using Bespoke.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumePro.Domain.Entities;

public sealed class Company : BaseEntity<Company>, ICompany
{
    public int OrganizationId { get; set; }
    public Persona Persona { get; set; } = null!;
    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public int PersonId { get; set; }
    public ICollection<ResumeCompany> Resumes { get; set; } = new List<ResumeCompany>();
    public ICollection<CompanySkill> Skills { get; set; } = new List<CompanySkill>();
    public ICollection<Position> Positions { get; set; } = new List<Position>();
    public ICollection<Highlight> Highlights { get; set; } = new List<Highlight>();
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string CompanyName { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string Description { get; set; } = null!;

    public override void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(x => new { x.OrganizationId, x.PersonId, x.Id });

        builder.Property(x => x.Description)
            .ConfigureColumn(StringColumnSize.Large, false);

        builder.Property(x => x.CompanyName)
            .ConfigureColumn(StringColumnSize.Small);

        builder.Property(x => x.Location)
            .ConfigureColumn(StringColumnSize.Small, false);

        builder.HasOne(x => x.Persona)
            .WithMany(x => x.Companies)
            .HasForeignKey(x => new { x.OrganizationId, x.PersonId })
            .HasPrincipalKey(x => new { x.OrganizationId, x.Id })
            .OnDelete(DeleteBehavior.NoAction);
    }
}