#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using ResumePro.Core.Data.Enums;
using ResumePro.Core.Data.Interfaces;

namespace ResumePro.Core.Data.Bases;

public abstract class BaseObjectState : IObjectState
{
    [NotMapped][IgnoreDataMember] public ObjectState ObjectState { get; set; }
}