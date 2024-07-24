#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Options;

namespace ResumePro.Api.Testing.TestData;

public class PersonSkillTestData
{
    public static IEnumerable<PersonaSkillsOptions> ValidOptions()
    {
        return new List<PersonaSkillsOptions>
        {
            new()
            {
                SkillId = 1
            }
        };
    }
}