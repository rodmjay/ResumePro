#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.ErrorDescribers;

public class ReferenceErrorDescriber
{
    public virtual Error ReferenceNotFound(int referenceId)
    {
        return new Error
        {
            Code = nameof(ReferenceNotFound)
        };
    }

    public virtual Error UnableToSaveReference()
    {
        return new Error
        {
            Code = nameof(UnableToSaveReference)
        };
    }
}