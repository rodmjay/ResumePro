#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.CodeAnalysis.CSharp.Syntax;
using ResumePro.Shared.Options;

namespace ResumePro.Api.Testing.TestData;

public class HighlightsTestData
{
    public static IEnumerable<HighlightCreateOptions> ValidCreateOptions()
    {
        return new List<HighlightCreateOptions>
        {
            new()
            {
                Text = "test"
            }
        };
    }

    public static IEnumerable<HighlightUpdateOptions> ValidUpdateOptions()
    {
        return new List<HighlightUpdateOptions>
        {
            new()
            {
                Order = 1,
                Text = "test"
            }
        };
    }
}