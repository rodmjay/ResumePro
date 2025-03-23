using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

[ApiController]
[Route("account")]
public class AccountController : ControllerBase
{
    //[HttpPost("login")]
    //public async Task<IActionResult> Login([FromBody] LoginModel model)
    //{
    //    // Validate credentials here (e.g. check username/password)

    //    // If valid, create the user principal
    //    var claims = new List<Claim>
    //    {
    //        new Claim(ClaimTypes.Name, model.Username),
    //        // Add additional claims as needed
    //    };
    //    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    //    var authProperties = new AuthenticationProperties
    //    {
    //        // Customize properties like IsPersistent, ExpiresUtc, etc.
    //    };

    //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
    //        new ClaimsPrincipal(claimsIdentity),
    //        authProperties);

    //    return Ok();
    //}

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok();
    }
}