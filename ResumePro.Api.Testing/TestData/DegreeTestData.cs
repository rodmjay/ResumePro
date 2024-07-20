#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Options;

namespace ResumePro.Api.Testing.TestData;

public class DegreeTestData
{
    public static IEnumerable<DegreeOptions> ValidOptions()
    {
        return new List<DegreeOptions>
        {
            new()
            {
                Name = "test"
            }
        };
    }
}