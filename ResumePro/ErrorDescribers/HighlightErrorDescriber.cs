#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.ErrorDescribers;

public class HighlightErrorDescriber
{
    public virtual Error HighlightNotFound(int resumeId)
    {
        return new Error
        {
            Code = nameof(HighlightNotFound)
        };
    }

    public virtual Error UnableToSaveHighlight()
    {
        return new Error
        {
            Code = nameof(UnableToSaveHighlight)
        };
    }
}