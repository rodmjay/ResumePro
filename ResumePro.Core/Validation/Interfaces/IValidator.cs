#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.ComponentModel.DataAnnotations;
using ResumePro.Core.Data.Interfaces;
using ResumePro.Core.Services.Interfaces;

namespace ResumePro.Core.Validation.Interfaces;

public interface IValidator<T> where T : class, IObjectState
{
    ValidationResult Validate(IService<T> service, T account, string value);
}