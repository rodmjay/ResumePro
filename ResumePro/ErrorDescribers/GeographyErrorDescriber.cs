﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.ErrorDescribers;

public class GeographyErrorDescriber
{
    public virtual Error EnableCountryError()
    {
        return new Error
        {
            Code = nameof(EnableCountryError),
            Description = "Unable to enable country"
        };
    }

    public virtual Error DisableCountryError()
    {
        return new Error
        {
            Code = nameof(DisableCountryError),
            Description = "Unable to disable country"
        };
    }

    public virtual Error CountryAlreadyEnabled()
    {
        return new Error
        {
            Code = nameof(CountryAlreadyEnabled),
            Description = "country already enabled"
        };
    }

    public virtual Error CountryAlreadyDisabled()
    {
        return new Error
        {
            Code = nameof(CountryAlreadyDisabled),
            Description = "country already disabled"
        };
    }
}