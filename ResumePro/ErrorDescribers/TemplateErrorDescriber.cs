﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.ErrorDescribers;

public class TemplateErrorDescriber
{
    public virtual Error TemplateNotFound(string templateId)
    {
        return new Error
        {
            Code = nameof(TemplateNotFound)
        };
    }
}