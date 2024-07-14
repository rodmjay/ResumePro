#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.ErrorDescribers;

public class SkillErrorDescriber
{
    public virtual Error SkillNotFound(int skillId)
    {
        return new Error
        {
            Code = nameof(SkillNotFound)
        };
    }


    public virtual Error UnableToSaveSkill()
    {
        return new Error
        {
            Code = nameof(UnableToSaveSkill)
        };
    }
}
