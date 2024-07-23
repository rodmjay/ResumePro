#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Options;

namespace ResumePro.Api.Testing.TestData;

public class ReferenceTestData
{
    public static IEnumerable<ReferenceOptions> ValidCreateOptions()
    {
        return new List<ReferenceOptions>
        {
            new()
            {
                Name = "test test",
                Text = "foo"
            }
        };
    }

    public static IEnumerable<ReferenceOptions> ValidOptions()
    {
        return new List<ReferenceOptions>
        {
            new()
            {
                Name = "test test",
                Order = 1,
                Text = "foo"
            }
        };
    }
}