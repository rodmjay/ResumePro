#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Common;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Interfaces;

public interface IPersonSkillsController
{
    Task<List<PersonaSkillDto>> GetSkills(int personId);
    Task<Result> AddOrUpdateSkill(int personId, PersonaSkillsOptions options);
    Task<Result> DeletePersonalSkill(int personId, int skillId);
}