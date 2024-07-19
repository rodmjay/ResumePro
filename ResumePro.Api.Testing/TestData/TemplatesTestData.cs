#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Options;

namespace ResumePro.Api.Testing.TestData;

public class TemplatesTestData
{
    public static IEnumerable<TemplateOptions> ValidOptions()
    {
        return new List<TemplateOptions>()
        {
            new TemplateOptions()
            {
                Name = "test",
                Engine = "hb",
                Format = "md",
                Template = "test"
            }
        };
    }
}