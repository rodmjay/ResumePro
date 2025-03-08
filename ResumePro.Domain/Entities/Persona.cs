#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Bespoke.Data.Enums;
using Bespoke.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Entities;

namespace ResumePro.Domain.Entities;

public sealed class Persona : BaseEntity<Persona>, IPersona
{
    public ICollection<Highlight> Highlights { get; set; } = new List<Highlight>();
    public ICollection<PersonaSkill> Skills { get; set; } = new List<PersonaSkill>();
    public ICollection<Resume> Resumes { get; set; } = new List<Resume>();
    public ICollection<Company> Companies { get; set; } = new List<Company>();
    public ICollection<School> Schools { get; set; } = new List<School>();
    public ICollection<Certification> Certifications { get; set; } = new List<Certification>();
    public ICollection<PersonaLanguage> Languages { get; set; } = new List<PersonaLanguage>();
    public StateProvince State { get; set; }
    public int StateId { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<Reference> References { get; set; } = new List<Reference>();

    public int OrganizationId { get; set; }
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string LinkedIn { get; set; }
    public string GitHub { get; set; }
    public string City { get; set; }

    public override void Configure(EntityTypeBuilder<Persona> builder)
    {
        builder.HasKey(x => new {x.OrganizationId, x.Id});

        builder.HasOne(x => x.State)
            .WithMany(x => x.People)
            .HasForeignKey(x => x.StateId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.FirstName)
            .ConfigureColumn(StringColumnSize.VerySmall);

        builder.Property(x => x.LastName)
            .ConfigureColumn(StringColumnSize.VerySmall);

        builder.Property(x => x.Email)
            .ConfigureColumn(StringColumnSize.VerySmall);

        builder.Property(x => x.PhoneNumber)
            .ConfigureColumn(StringColumnSize.VerySmall);

        builder.Property(x => x.GitHub)
            .ConfigureColumn(StringColumnSize.VerySmall, false);

        builder.Property(x => x.LinkedIn)
            .ConfigureColumn(StringColumnSize.VerySmall, false);

        builder.HasQueryFilter(x => x.IsDeleted == false);
    }
}