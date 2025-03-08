#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Models;

public class SkillDto : ISkill
{
    public List<string> Categories { get; set; } = new();
    public int Id { get; set; }

    public string Title { get; set; }
}