#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Hosting;
using ResumePro.Users.Managers;

namespace ResumePro.IdentityServer.Pages.Account;

[AllowAnonymous]
public class RegisterConfirmationModel : PageModel
{
    private readonly IEmailSender _sender;
    private readonly UserManager _userManager;

    public RegisterConfirmationModel(UserManager userManager, IEmailSender sender, IWebHostEnvironment environment)
    {
        _userManager = userManager;
        _sender = sender;
        DisplayConfirmAccountLink = environment.IsDevelopment() || environment.EnvironmentName == "Testing";
    }

    public string Email { get; set; }

    public bool DisplayConfirmAccountLink { get; set; }

    public string EmailConfirmationUrl { get; set; }

    public async Task<IActionResult> OnGetAsync(string email, string returnUrl = null)
    {
        if (email == null) return RedirectToPage("/Index");

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return NotFound($"Unable to load user with email '{email}'.");

        Email = email;
        // Once you add a real email sender, you should remove this code that lets you confirm the account
        if (DisplayConfirmAccountLink)
        {
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            EmailConfirmationUrl = Url.Page(
                "/Account/ConfirmEmail",
                null,
                new {userId, code, returnUrl},
                Request.Scheme);
        }

        return Page();
    }
}