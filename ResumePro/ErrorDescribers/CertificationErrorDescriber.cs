#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.ErrorDescribers;

public class CertificationErrorDescriber
{
    public virtual Error CertificationNotFound(int certificationId)
    {
        return new Error
        {
            Code = nameof(CertificationNotFound)
        };
    }


    public virtual Error UnableToSaveCertification()
    {
        return new Error
        {
            Code = nameof(UnableToSaveCertification)
        };
    }
}