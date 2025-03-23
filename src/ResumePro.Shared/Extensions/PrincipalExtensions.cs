using System.Diagnostics;
using System.Security.Claims;
using System.Security.Principal;

namespace ResumePro.Shared.Extensions;

/// <summary>
///     Extension methods for <see cref="IPrincipal" /> and
///     <see cref="IIdentity" /> .
/// </summary>
public static class PrincipalExtensions
{
    /// <summary>
    ///     Determines whether this instance is authenticated.
    /// </summary>
    /// <param name="principal">The principal.</param>
    /// <returns>
    ///     <c>true</c> if the specified principal is authenticated; otherwise, <c>false</c>.
    /// </returns>
    [DebuggerStepThrough]
    public static bool IsAuthenticated(this IPrincipal principal)
    {
        return principal is { Identity: { IsAuthenticated: true } };
    }

    public static int UserId(this ClaimsPrincipal principal)
    {
        // Attempt to retrieve the organizationId claim as a string
        var userIdClaim = principal.Identity!.Name;

        // Try parsing the claim value to an integer
        if (int.TryParse(userIdClaim, out var userId))
            return userId;
        throw new Exception("The userId claim is missing or not a valid integer.");
    }

    public static int OrganizationId(this ClaimsPrincipal principal)
    {
        // Attempt to retrieve the organizationId claim as a string
        var organizationIdClaim = principal.Claims.FirstOrDefault(c => c.Type == "organizationId")?.Value;

        // Try parsing the claim value to an integer
        if (int.TryParse(organizationIdClaim, out var organizationId))
            return organizationId;
        throw new Exception("The organizationId claim is missing or not a valid integer.");
    }
}