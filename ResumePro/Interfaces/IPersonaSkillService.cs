﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Services.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Interfaces;

public interface IPersonaSkillService : IService<PersonaSkill>
{
    Task<List<T>> GetPersonaSkills<T>(int organizationId, int personId) where T : PersonaSkillDto;

    Task<Result> TogglePersonalSkill(int organizationId, int personId, int skillId);

    Task<Result> DeletePersonalSkill(int organizationId, int personId, int skillId);
}