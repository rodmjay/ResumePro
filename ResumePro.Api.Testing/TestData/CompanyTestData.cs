#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Options;

namespace ResumePro.Api.Testing.TestData;

public class CompanyTestData
{
    public static IEnumerable<CompanyOptions> ValidOptions()
    {
        return new List<CompanyOptions>
        {
            new()
            {
                Company = "asdf",
                Description = "test",
                EndDate = DateTime.Parse("1/1/2015"),
                StartDate = DateTime.Parse("1/1/2011"),
                Location = "Seattle, WA"
            },
            new()
            {
                Company = "company2",
                Description = "test2",
                EndDate = null,
                StartDate = DateTime.Parse("1/1/2014"),
                Location = "Seattle, WA"
            }
        };
    }
}