#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ResumePro.Shared.Common;

namespace ResumePro.Api.Testing.Extensions;

public static class ActionResultExtensions
{
    public static T GetObject<T>(this ActionResult<T> response) where T : class
    {
        Assert.That(response.Result is OkObjectResult, Is.True);

        T myResult = (((OkObjectResult)response.Result!)!.Value as T)!;
        return myResult;
    }

    public static Result GetResult<TOrig>(this ActionResult<TOrig> response)
    {
        Assert.That(response.Result is BadRequestObjectResult, Is.True);
        Result myResult = (((BadRequestObjectResult)response.Result!)!.Value as Result)!;
        return myResult;
    }
}