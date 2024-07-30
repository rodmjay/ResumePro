#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using Dawn;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ResumePro.Core.Extensions;
using ResumePro.Core.Validation;
using ResumePro.Shared.Common;

namespace ResumePro.Core.Middleware;

public class ExceptionMiddleware(
    RequestDelegate next,
    ILoggerFactory loggerFactory,
    IOptions<MvcNewtonsoftJsonOptions> jsonOptions)
{
    private readonly JsonSerializerSettings _jsonSerializerSettings = jsonOptions.Value.SerializerSettings;


    private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
    {
        return $"[{nameof(ExceptionMiddleware)}.{callerName}] - {message}";
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static HttpStatusCode GetErrorCode(Exception e)
    {
        switch (e)
        {
            case UnauthorizedAccessException _:
                return HttpStatusCode.Unauthorized;
            case ValidationException _:
                return HttpStatusCode.BadRequest;
            case FormatException _:
                return HttpStatusCode.BadRequest;
            case AuthenticationException _:
                return HttpStatusCode.Forbidden;
            case NotImplementedException _:
                return HttpStatusCode.NotImplemented;
            default:
                return HttpStatusCode.InternalServerError;
        }
    }

    private void LogAndAddException(Result modelResult, Exception exception)
    {
        Guard.Argument(exception)
            .NotNull();

        var exLogger = loggerFactory.CreateLogger(exception.TargetSite!.DeclaringType!.FullName!);
        exLogger?.LogError(exception, exception.Message);
        modelResult.Errors.Add(new Error
        {
            Code = GetErrorCode(exception).ToString(),
            Description = exception.Message
        });
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var modelResult = new Result();

        if (exception is AggregateException exceptions)
            foreach (var ex in exceptions.InnerExceptions)
                LogAndAddException(modelResult, ex);
        else
            LogAndAddException(modelResult, exception);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)GetErrorCode(exception);

        return context.Response.WriteAsync(modelResult.ToJson(_jsonSerializerSettings));
    }
}