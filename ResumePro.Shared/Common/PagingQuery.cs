#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Common;

public class PagingQuery
{
    public string Sort { get; set; }
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
}