#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Data.Enums;

namespace ResumePro.Core.Data.Interfaces;

public interface IObjectState
{
    public ObjectState ObjectState { get; set; }
}