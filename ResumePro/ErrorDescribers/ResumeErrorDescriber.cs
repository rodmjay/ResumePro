#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.ErrorDescribers;

public class ResumeErrorDescriber
{
    public virtual Error ResumeNotFound(int resumeId)
    {
        return new Error
        {
            Code = nameof(ResumeNotFound)
        };
    }

    public virtual Error UnableToSaveResume()
    {
        return new Error
        {
            Code = nameof(UnableToSaveResume)
        };
    }
}