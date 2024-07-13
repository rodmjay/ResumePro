#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.ErrorDescribers;

public class ProjectErrorDescriber
{
    public virtual Error ProjectNotFound(int projectId)
    {
        return new Error
        {
            Code = nameof(ProjectNotFound)
        };
    }


    public virtual Error UnableToSaveProject()
    {
        return new Error
        {
            Code = nameof(UnableToSaveProject)
        };
    }
}