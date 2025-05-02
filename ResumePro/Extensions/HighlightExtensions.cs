#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Extensions;

internal static class HighlightExtensions
{
    public static Highlight Update(this Highlight highlight, HighlightOptions options, int order)
    {
        highlight.Text = options.Text;
        highlight.ObjectState = ObjectState.Modified;
        highlight.Order = order;

        return highlight;
    }

    public static ProjectHighlight CreateProjectHighlight(this HighlightOptions options, Func<Task<int>> newId)
    {
        var nextId = newId().Result;

        return new ProjectHighlight()
        {
            Id = nextId,
            ObjectState = ObjectState.Added,
            Order = nextId,
            Text = options.Text
        };
    }

    public static Highlight Create(this HighlightOptions options, Func<Task<int>> newId)
    {
        var nextId = newId().Result;

        return new Highlight()
        {
            Id = nextId,
            ObjectState = ObjectState.Added,
            Order = nextId,
            Text = options.Text
        };
    }

}