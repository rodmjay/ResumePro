namespace ResumePro.Api.Controllers;

public sealed class IdentityController : BaseController
{
    public IdentityController(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    [HttpGet]
    public IActionResult GetIdentity()
    {
        return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
    }
}