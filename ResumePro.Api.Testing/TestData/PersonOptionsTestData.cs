#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Options;

namespace ResumePro.Api.Testing.TestData;

public class PersonOptionsTestData
{
    public static IEnumerable<PersonaOptions> ValidOptions()
    {
        return new List<PersonaOptions>
        {
            new()
            {
                City = "Test",
                Email = "test@test.com",
                FirstName = "Foo",
                LastName = "Bar",
                GitHub = "https://www.github.com/foo/bar",
                LinkedIn = "https://www.linkedin.com/in/foobar",
                StateId = 45
            }
        };
    }
}