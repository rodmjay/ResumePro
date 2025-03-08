#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion


namespace ResumePro.Shared.Interfaces;

public interface IPersonSkillsController
{
    Task<List<PersonaSkillDto>> GetSkills(int personId);
    Task<Result> ToggleSkill(int personId, int skillId);
}