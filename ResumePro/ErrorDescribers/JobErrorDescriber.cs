#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.ErrorDescribers;

public class JobErrorDescriber
{
    public virtual Error JobNotFound(int jobId)
    {
        return new Error
        {
            Code = nameof(JobNotFound)
        };
    }


    public virtual Error UnableToSaveJob()
    {
        return new Error
        {
            Code = nameof(UnableToSaveJob)
        };
    }
}