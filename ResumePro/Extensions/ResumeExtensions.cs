#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared;

namespace ResumePro.Extensions;

public static class ResumeExtensions
{
    public static string GetLanguageString(this ResumeDetails details)
    {
        return string.Join(", ", details.Languages.OrderByDescending(a => a.Proficiency)
            .Select(language => $"{language.LanguageName}").ToList());
    }

    public static string GetFileName(this ResumeDto resume)
    {
        var fileName = $"{resume.FirstName} {resume.LastName}-{resume.JobTitle}.pdf";

        return fileName;
    }
}