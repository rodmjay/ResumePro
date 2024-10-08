﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Options;

namespace ResumePro.Api.Testing.TestData;

public class JobTestData
{
    public static IEnumerable<JobOptions> ValidOptions()
    {
        return new List<JobOptions>
        {
            new()
            {
                Company = "asdf",
                Description = "test",
                EndDate = DateTime.Parse("1/1/2015"),
                StartDate = DateTime.Parse("1/1/2011"),
                Location = "Seattle, WA",
                JobTitle = "foo"
            },
            new()
            {
                Company = "company2",
                Description = "test2",
                EndDate = null,
                StartDate = DateTime.Parse("1/1/2014"),
                Location = "Seattle, WA",
                JobTitle = "architect"
            }
        };
    }
}