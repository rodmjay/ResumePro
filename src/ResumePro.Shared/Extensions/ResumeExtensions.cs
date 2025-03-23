#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Extensions;

public static class ResumeExtensions
{
    public static string? GetLanguageString(this ResumeDetails details)
    {
        if (details.Languages == null || !details.Languages.Any())
            return null;

        return string.Join(", ", details.Languages.OrderByDescending(a => a.Proficiency)
            .Select(language => $"{language.LanguageName}").ToList());
    }

    public static string GetFileName(this ResumeDto resume)
    {
        var fileName = $"{resume.FirstName} {resume.LastName}-{resume.JobTitle}.pdf";

        return fileName;
    }
}