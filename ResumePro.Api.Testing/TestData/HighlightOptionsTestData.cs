#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.DiaSymReader;
using ResumePro.Shared.Options;

namespace ResumePro.Api.Testing.TestData;

public class HighlightOptionsTestData
{
    public static IEnumerable<HighlightCreateOptions> ValidCreateOptions()
    {
        return new List<HighlightCreateOptions>()
        {
            new()
            {
                Text = "test"
            }
        };
    }
    public static IEnumerable<HighlightUpdateOptions> ValidUpdateOptions()
    {
        return new List<HighlightUpdateOptions>()
        {
            new()
            {
                Order = 1,
                Text = "test"
            }
        };
    }
}