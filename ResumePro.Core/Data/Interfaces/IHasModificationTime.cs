#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Core.Data.Interfaces;

public interface IHasModificationTime
{
    /// <summary>The last modified time for this entity.</summary>
    DateTime? Updated { get; set; }
}