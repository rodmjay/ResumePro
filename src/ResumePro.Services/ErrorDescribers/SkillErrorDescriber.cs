namespace ResumePro.Services.ErrorDescribers;

public class SkillErrorDescriber
{
    public virtual Error SkillNotFound(int skillId)
    {
        return new Error
        {
            Code = nameof(SkillNotFound),
            Description = null
        };
    }


    public virtual Error UnableToSaveSkill()
    {
        return new Error
        {
            Code = nameof(UnableToSaveSkill),
            Description = null
        };
    }
}