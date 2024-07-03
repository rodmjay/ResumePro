#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Core.Data.Interfaces;

public interface IHasCreationTime
{
    /// <summary>Creation time of this entity.</summary>
    DateTime Created { get; set; }
}