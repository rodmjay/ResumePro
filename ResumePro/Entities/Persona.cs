#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Runtime.ConstrainedExecution;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;
using ResumePro.Geography.Entities;
using ResumePro.Languages.Entities;
using ResumePro.Shared.Interfaces;

namespace ResumePro.Entities;

public class PersonaLanguage : BaseEntity<PersonaLanguage>
{
    public int OrganizationId { get; set; }
    public int PersonaId { get; set; }
    public Persona Persona { get; set; }
    public string Code3 { get; set; }
    public Language Language { get; set; }

    public int Proficiency { get; set; }

    public override void Configure(EntityTypeBuilder<PersonaLanguage> builder)
    {
        builder.HasKey(x => new {x.OrganizationId, x.PersonaId, x.Code3});

        builder.HasOne(x => x.Persona)
            .WithMany(x => x.Languages)
            .HasForeignKey(x => new {x.OrganizationId, x.PersonaId})
            .HasPrincipalKey(x => new {x.OrganizationId, x.Id})
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Language)
            .WithMany(x => x.People)
            .HasForeignKey(x => x.Code3)
            .HasPrincipalKey(x => x.Code3);
    }
}

public class Persona : BaseEntity<Persona>, IPersona
{
    public ICollection<PersonaSkill> Skills { get; set; }
    public ICollection<Resume> Resumes { get; set; }
    public ICollection<Job> Jobs { get; set; }
    public ICollection<School> Schools { get; set; }
    public ICollection<Certification> Certifications { get; set; }
    public ICollection<PersonaLanguage> Languages { get; set; }
    public int OrganizationId { get; set; }
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string LinkedIn { get; set; }
    public string GitHub { get; set; }
    public string City { get; set; }

    public StateProvince State { get; set; }
    public int StateId { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<Reference> References { get; set; } = new List<Reference>();


    public override void Configure(EntityTypeBuilder<Persona> builder)
    {
        builder.HasKey(x => new{ x.OrganizationId, x.Id});

        builder.HasOne(x => x.State)
            .WithMany(x => x.People)
            .HasForeignKey(x => x.StateId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasQueryFilter(x => x.IsDeleted == false);
    }
}