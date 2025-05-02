#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.ErrorDescribers;

public class SchoolErrorDescriber
{
    public virtual Error SchoolNotFound(int SchoolId)
    {
        return new Error
        {
            Code = nameof(SchoolNotFound),
            Description = "School Not Found"
        };
    }


    public virtual Error UnableToSaveSchool()
    {
        return new Error
        {
            Code = nameof(UnableToSaveSchool),
            Description = "Unable to save school"
        };
    }
}