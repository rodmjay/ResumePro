#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;

namespace ResumePro.Shared.Extensions;

public static class ActionResultExtensions
{
    public static bool IsSuccessStatusCode<T>(this ActionResult<T> result)
    {
        return result.Result is OkObjectResult;
    }

    public static T GetObject<T>(this ActionResult<T> response) where T : class
    {
        var myResult = (((OkObjectResult) response.Result!)!.Value as T)!;
        return myResult;
    }

    public static Result GetResult<TOrig>(this ActionResult<TOrig> response)
    {
        var myResult = (((BadRequestObjectResult) response.Result!)!.Value as Result)!;
        return myResult;
    }
}