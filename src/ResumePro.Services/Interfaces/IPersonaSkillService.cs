using Bespoke.Services.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Interfaces;

public interface IPersonaSkillService : IService<PersonaSkill>
{
    Task<List<T>> GetPersonaSkills<T>(int organizationId, int personId) where T : PersonaSkillDto;

    Task<Result> TogglePersonalSkill(int organizationId, int personId, int skillId);

    Task<Result> DeletePersonalSkill(int organizationId, int personId, int skillId);
}