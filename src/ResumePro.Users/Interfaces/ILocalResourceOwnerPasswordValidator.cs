using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumePro.Users.Interfaces;

public interface ILocalResourceOwnerPasswordValidator
{
    Task ValidateAsync(LocalResourceOwnerPasswordValidationContext context);
}

public class LocalResourceOwnerPasswordValidationContext
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public LocalGrantValidationResult? Result { get; set; }
}

public class LocalGrantValidationResult
{
    public bool IsError { get; }
    public string? Error { get; }
    public string? ErrorDescription { get; }
    public string? Subject { get; }
    public string? AuthenticationMethod { get; }
    public IEnumerable<Claim> Claims { get; }

    public LocalGrantValidationResult(string subject, string authenticationMethod, IEnumerable<Claim>? claims = null)
    {
        Subject = subject;
        AuthenticationMethod = authenticationMethod;
        Claims = claims ?? Enumerable.Empty<Claim>();
        IsError = false;
    }

    public LocalGrantValidationResult(string error, string? errorDescription = null)
    {
        Error = error;
        ErrorDescription = errorDescription;
        IsError = true;
        Subject = null;
        AuthenticationMethod = null;
        Claims = Enumerable.Empty<Claim>();
    }
}

public static class LocalTokenRequestErrors
{
    public const string InvalidGrant = "invalid_grant";
    public const string InvalidRequest = "invalid_request";
    public const string InvalidClient = "invalid_client";
    public const string InvalidScope = "invalid_scope";
    public const string UnauthorizedClient = "unauthorized_client";
    public const string UnsupportedGrantType = "unsupported_grant_type";
}
