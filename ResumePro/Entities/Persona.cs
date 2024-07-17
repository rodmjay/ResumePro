#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;
using ResumePro.Shared.Interfaces;

namespace ResumePro.Entities;

public sealed class Persona : BaseEntity<Persona>, IPersona
{
    public ICollection<PersonaSkill> Skills { get; set; }
    public ICollection<Resume> Resumes { get; set; }
    public ICollection<Job> Jobs { get; set; }
    public ICollection<School> Schools { get; set; }
    public ICollection<Certification> Certifications { get; set; }
    public ICollection<PersonaLanguage> Languages { get; set; }
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

        builder.HasQueryFilter(x => x.IsDeleted == false);
    }
}