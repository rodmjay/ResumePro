#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.ErrorDescribers;

public class CompanyErrorDescriber
{
    public virtual Error CompanyNotFound(int companyId)
    {
        return new Error
        {
            Code = nameof(CompanyNotFound)
        };
    }


    public virtual Error UnableToSaveCompany()
    {
        return new Error
        {
            Code = nameof(UnableToSaveCompany)
        };
    }
}