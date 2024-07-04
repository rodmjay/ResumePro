#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Services.Interfaces;
using ResumePro.Entities;
using ResumePro.Shared;

namespace ResumePro.Interfaces;

public interface ISkillService : IService<Skill>
{
    Task<List<T>> GetSkills<T>() where T : SkillDto;
}