#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Options;

namespace ResumePro.Api.Testing.TestData;

public class CertificationsTestData
{
    public static IEnumerable<CertificationOptions> ValidOptions()
    {
        return new List<CertificationOptions>()
        {
            new()
            {
                Body = "test",
                Date = DateTime.Parse("1/1/2010"),
                Name = "foo"
            }
        };
    }
}