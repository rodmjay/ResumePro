#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;
using ResumePro.Shared;

namespace ResumePro.Entities;

public class Persona : BaseEntity<Persona>, IPersona
{
    public ICollection<PersonaSkill> Skills { get; set; }
    public ICollection<Resume> Resumes { get; set; }
    public ICollection<Job> Jobs { get; set; }
    public ICollection<School> Schools { get; set; }
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string LinkedIn { get; set; }
    public string GitHub { get; set; }
    public string City { get; set; }
    public string State { get; set; }

    public override void Configure(EntityTypeBuilder<Persona> builder)
    {
        builder.HasKey(x => x.Id);
    }
}