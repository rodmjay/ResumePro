using Bespoke.Core.Settings;
using Microsoft.Extensions.Options;
using ResumePro.Shared.Extensions;

namespace ResumePro.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("v1.0/[controller]")]
public class BaseController : ControllerBase
{
    protected readonly AppSettings AppSettings;

    /// <param name="serviceProvider"></param>
    protected BaseController(IServiceProvider serviceProvider)
    {
        AppSettings = serviceProvider.GetRequiredService<IOptions<AppSettings>>().Value;
    }

    public int UserId => User.UserId();

    public int OrganizationId => User.Identity?.IsAuthenticated == true ? User.OrganizationId() : 1; // Default to org ID 1 for development
}
