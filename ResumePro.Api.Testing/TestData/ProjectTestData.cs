#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Options;

namespace ResumePro.Api.Testing.TestData;

public class ProjectTestData
{
    public static IEnumerable<ProjectOptions> ValidOptions()
    {
        return new List<ProjectOptions>
        {
            new()
            {
                Name = "test",
                Budget = null,
                Description = "foo",
                Order = 1
            }
        };
    }
}