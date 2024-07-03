#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Core.Validation.Interfaces;

public interface IValidationContainer
{
    IDictionary<string, IList<string>> ValidationErrors { get; }
    bool IsValid { get; }
}

public interface IValidationContainer<out T> : IValidationContainer
{
    T Entity { get; }
}