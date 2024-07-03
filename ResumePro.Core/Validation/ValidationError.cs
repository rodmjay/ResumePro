#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Newtonsoft.Json;

namespace ResumePro.Core.Validation;

public class ValidationError
{
    public ValidationError(string field, string message)
    {
        Field = field != string.Empty ? field : null;
        Message = message;
    }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string Field { get; }

    public string Message { get; }
}