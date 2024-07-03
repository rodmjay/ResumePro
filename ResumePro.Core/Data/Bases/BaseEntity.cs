#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResumePro.Core.Data.Enums;
using ResumePro.Core.Data.Interfaces;

namespace ResumePro.Core.Data.Bases;

public abstract class BaseEntity : IObjectState
{
    [NotMapped] [IgnoreDataMember] public ObjectState ObjectState { get; set; }
}

public abstract class BaseEntity<T> : BaseEntity, IEntityTypeConfiguration<T> where T : class
{
    public abstract void Configure(EntityTypeBuilder<T> builder);
}