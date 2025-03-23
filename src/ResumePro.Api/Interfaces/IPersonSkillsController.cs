namespace ResumePro.Api.Interfaces;

public interface IPersonSkillsController
{
    Task<List<PersonaSkillDto>> GetSkills(int personId);
    Task<Result> ToggleSkill(int personId, int skillId);
}