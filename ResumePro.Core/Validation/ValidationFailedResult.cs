#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ResumePro.Core.Validation;

public class ValidationFailedResult : ObjectResult
{
    public ValidationFailedResult(ModelStateDictionary modelState)
        : base(new ValidationResultModel(modelState))
    {
        StatusCode = StatusCodes.Status422UnprocessableEntity;
    }
}