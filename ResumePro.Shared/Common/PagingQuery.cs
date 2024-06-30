#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Common;

public class PagingQuery
{
    public string Sort { get; set; }
    public int Page { get; set; }
    public int Size { get; set; }
}