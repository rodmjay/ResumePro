#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Core.Data.Interfaces;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}