#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Options;

namespace ResumePro.Api.Testing.TestData;

public class HighlightsTestData
{
    public static IEnumerable<HighlightOptions> ValidCreateOptions()
    {
        return new List<HighlightOptions>
        {
            new()
            {
                Text = "test"
            }
        };
    }

    public static IEnumerable<HighlightOptions> ValidUpdateOptions()
    {
        return new List<HighlightOptions>
        {
            new()
            {
                Order = 1,
                Text = "test"
            }
        };
    }
}