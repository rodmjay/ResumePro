#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Options;

public class CreateResumeOptions
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool AttachAllJobs { get; set; } = true;
    public bool AttachAllSkills { get; set; } = true;
}