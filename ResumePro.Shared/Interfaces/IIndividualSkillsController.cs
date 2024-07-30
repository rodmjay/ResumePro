#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Common;
using ResumePro.Shared.Models;

namespace ResumePro.Shared.Interfaces;

public interface IIndividualSkillsController
{
    Task<List<PersonaSkillDto>> GetSkills();
    Task<Result> ToggleSkill(int skillId);
}