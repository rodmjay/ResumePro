#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared;

public interface IHighlight
{
    int Id { get; set; }
    int Order { get; set; }
    string Text { get; set; }
}