#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.ComponentModel.DataAnnotations;

namespace ResumePro.Shared.Options;

public class ResumeOptions : IDescription, IJobTitle
{
    [Required] public string JobTitle { get; set; }

    [Required] public string Description { get; set; }

    public ResumeSettingsOptions Settings { get; set; } = new();

    public List<int> SkillIds { get; set; } = new();

    public List<int> CompanyIds { get; set; } = new();
}