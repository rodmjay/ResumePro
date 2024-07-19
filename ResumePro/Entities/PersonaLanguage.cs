#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Bases;
using ResumePro.Languages.Entities;
using ResumePro.Shared.Interfaces;

namespace ResumePro.Entities;

public sealed class PersonaLanguage : BaseEntity<PersonaLanguage>, IPersonaLanguage
{
    public Persona Persona { get; set; }
    public Language Language { get; set; }
    public int OrganizationId { get; set; }
    public int PersonaId { get; set; }
    public string Code3 { get; set; }

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