#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Text.RegularExpressions;
using ResumePro.Shared;

namespace ResumePro.Extractor;

public class ResumeParser
{
    public ResumeDetails Parse(string text)
    {
        var resume = new ResumeDetails();

        var name = Regex.Match(text, @"^\w+ \w+", RegexOptions.Multiline).Value;

        resume.Email = Regex.Match(text, @"Email: (\S+)").Groups[1].Value;
        resume.GitHub = Regex.Match(text, @"GitHub: (\S+)").Groups[1].Value;
        resume.LinkedIn = Regex.Match(text, @"LinkedIn: (\S+)").Groups[1].Value;
        resume.PhoneNumber = Regex.Match(text, @"Phone: (\S+)").Groups[1].Value;

        var education = Regex.Match(text, @"Education\s+(.*?)(?=\n\w+)", RegexOptions.Singleline).Groups[1].Value.Trim();
        

        return resume;
    }
}