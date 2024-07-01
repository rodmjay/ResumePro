#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared;

public class SkillDto : ISkill
{
    [JsonIgnore] public int Id { get; set; }

    public string Title { get; set; }
}