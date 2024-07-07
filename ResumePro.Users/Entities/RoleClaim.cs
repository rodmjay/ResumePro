#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumePro.Users.Entities;

public class RoleClaim : IdentityRoleClaim<int>, IObjectState, IEntityTypeConfiguration<RoleClaim>
{
    public Role Role { get; set; }

    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Role)
            .WithMany(x => x.RoleClaims)
            .HasForeignKey(x => x.RoleId);
    }

    [NotMapped] public ObjectState ObjectState { get; set; }
}