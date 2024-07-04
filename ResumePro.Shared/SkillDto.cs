#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Interfaces;

namespace ResumePro.Shared;

public class SkillDto : ISkill
{
    public int Id { get; set; }

    public string Title { get; set; }
}