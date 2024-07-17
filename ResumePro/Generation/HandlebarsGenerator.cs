#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using HandlebarsDotNet;
using ResumePro.Shared.Models;

namespace ResumePro.Generation;

public class HandlebarsGenerator : IResumeGenerator
{
    public string ExecuteOperation(ResumeDetails resume)
    {
        var source = "Hello, {{firstName}}";

        var template = Handlebars.Compile(source);

        var result = template(resume);

        return result;
    }
}