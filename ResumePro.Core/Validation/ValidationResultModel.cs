#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ResumePro.Core.Validation;

public class ValidationResultModel
{
    public ValidationResultModel()
    {
        Message = "Internal Server Error";
        Errors = new List<ValidationError>();
    }

    public ValidationResultModel(ModelStateDictionary modelState)
    {
        Message = "Validation Failed";
        Errors = modelState.Keys
            .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
            .ToList();
    }

    public string Message { get; }

    public ICollection<ValidationError> Errors { get; }
}