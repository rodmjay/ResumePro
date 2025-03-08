#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Models;

public class HighlightDto : IHighlight
{
    public int Id { get; set; }

    public int Order { get; set; }

    public string Text { get; set; }
}