namespace ResumePro.Api.Interfaces;

public interface ISkillsController
{
    Task<List<SkillDto>> GetSkills();
}