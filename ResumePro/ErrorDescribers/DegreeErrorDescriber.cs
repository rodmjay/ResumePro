﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.ErrorDescribers;

public class DegreeErrorDescriber
{
    public virtual Error DegreeNotFound(int degreeId)
    {
        return new Error
        {
            Code = nameof(DegreeNotFound)
        };
    }


    public virtual Error UnableToSaveDegree()
    {
        return new Error
        {
            Code = nameof(UnableToSaveDegree)
        };
    }
}