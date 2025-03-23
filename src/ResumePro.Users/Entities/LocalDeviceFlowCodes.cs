#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System;
using Bespoke.Data.Bases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumePro.Users.Entities;

public class LocalDeviceFlowCodes : BaseEntity<LocalDeviceFlowCodes>
{
    public int Id { get; set; }
    public string UserCode { get; set; }
    public string DeviceCode { get; set; }
    public string SubjectId { get; set; }
    public string SessionId { get; set; }
    public string ClientId { get; set; }
    public string Description { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? Expiration { get; set; }
    public string Data { get; set; }
    
    public override void Configure(EntityTypeBuilder<LocalDeviceFlowCodes> builder)
    {
        builder.ToTable("DeviceFlowCodes", "IdentityServer");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.UserCode)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(x => x.DeviceCode)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(x => x.SubjectId)
            .HasMaxLength(200);
        
        builder.Property(x => x.SessionId)
            .HasMaxLength(100);
        
        builder.Property(x => x.ClientId)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(x => x.Description)
            .HasMaxLength(200);
        
        builder.Property(x => x.CreationTime)
            .IsRequired();
        
        builder.Property(x => x.Expiration)
            .IsRequired();
        
        builder.Property(x => x.Data)
            .IsRequired();
    }
}
