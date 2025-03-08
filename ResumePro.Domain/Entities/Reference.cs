#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using Bespoke.Data.Enums;
using Bespoke.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Domain.Entities;

namespace ResumePro.Entities;

public sealed class Reference : BaseEntity<Reference>, IReference
{
    public int OrganizationId { get; set; }
 
    public int PersonId { get; set; }
    public Persona Persona { get; set; }

    public int Id { get; set; }
    public string Text { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public int Order { get; set; }

    public override void Configure(EntityTypeBuilder<Reference> builder)
    {

        builder.HasKey(x => new{x.OrganizationId, x.PersonId, x.Id});

        builder.Property(x => x.Name)
            .ConfigureColumn(StringColumnSize.Small);

        builder.Property(x => x.Text)
            .ConfigureColumn(StringColumnSize.Large);
        
        builder.HasOne(x => x.Persona)
            .WithMany(x => x.References)
            .HasForeignKey(x => new{x.OrganizationId, x.PersonId})
            .HasPrincipalKey(x=>new{x.OrganizationId, x.Id})
            .OnDelete(DeleteBehavior.Cascade);
    }
}