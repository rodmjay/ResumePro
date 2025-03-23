using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ResumePro.Users.Interfaces;

public interface ILocalProfileService
{
    Task GetProfileDataAsync(LocalProfileDataRequestContext context);
    Task IsActiveAsync(LocalIsActiveContext context);
}

public class LocalProfileDataRequestContext
{
    public ClaimsPrincipal Subject { get; set; } = null!;
    public List<string> RequestedClaimTypes { get; set; } = new List<string>();
    public List<Claim> IssuedClaims { get; set; } = new List<Claim>();
}

public class LocalIsActiveContext
{
    public ClaimsPrincipal Subject { get; set; } = null!;
    public bool IsActive { get; set; }
}
