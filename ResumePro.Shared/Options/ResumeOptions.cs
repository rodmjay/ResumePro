#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Diagnostics.CodeAnalysis;

namespace ResumePro.Shared.Options;

public class ResumeOptions
{
    [NotNull]
    public string Title { get; set; }

    [NotNull]
    public string Description { get; set; }
    public bool AttachAllJobs { get; set; } = true;
    public bool AttachAllSkills { get; set; } = true;
}