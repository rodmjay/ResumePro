#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Options;

namespace ResumePro.Api.Testing.TestData;

public class SchoolOptionsTestData
{
    public static IEnumerable<SchoolOptions> ValidOptions()
    {
        return new List<SchoolOptions>
        {
            new()
            {
                StartDate = DateTime.Parse("1/1/2000"),
                EndDate = DateTime.Parse("1/1/2024"),
                Name = "Test"
            }
        };
    }
}