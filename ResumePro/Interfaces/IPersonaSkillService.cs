﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Services.Interfaces;
using ResumePro.Entities;
using ResumePro.Shared;
using ResumePro.Shared.Common;
using ResumePro.Shared.Options;

namespace ResumePro.Interfaces;

public interface IPersonaSkillService : IService<PersonaSkill>
{
    Task<List<T>> GetPersonaSkills<T>(int organizationId, int personId) where T : PersonaSkillDto;

    Task<Result> AddOrUpdatePersonaSkill(int organizationId, int personId, PersonaSkillsOptions options);

    Task<Result> DeletePersonalSkill(int organizationId, int personId, int skillId);
}