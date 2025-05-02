#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Shared.Interfaces;

namespace ResumePro.Entities;

public sealed class Company : BaseEntity<Company>, ICompany
{
    public int OrganizationId { get; set; }
    public Persona Persona { get; set; }
    public int PersonId { get; set; }
    public ICollection<CompanySkill> Skills { get; set; } = new List<CompanySkill>();
    public ICollection<Position> Positions { get; set; } = new List<Position>();
    public ICollection<ResumeCompany> Resumes { get; set; } = new List<ResumeCompany>();
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string CompanyName { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }

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