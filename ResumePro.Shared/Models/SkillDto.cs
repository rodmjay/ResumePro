#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Interfaces;

namespace ResumePro.Shared.Models;

public class SkillDto : ISkill
{
    public string[] Categories { get; set; }
    public int Id { get; set; }

    public string Title { get; set; }
}