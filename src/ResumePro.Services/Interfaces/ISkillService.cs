using Bespoke.Services.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Interfaces;

public interface ISkillService : IService<Skill>
{
    Task<List<T>> GetSkills<T>() where T : SkillDto;
    Task<List<SkillDto>> GetSkillsDropdown();
}