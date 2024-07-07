#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Threading.Tasks;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ResumePro.Users.Managers;

namespace ResumePro.IdentityServer.Pages.Account;

public class LogOutModel : PageModel
{
    private readonly IClientStore _clientStore;
    private readonly IEventService _events;
    private readonly IIdentityServerInteractionService _interaction;
    private readonly ILogger<LoginModel> _logger;
    private readonly IAuthenticationSchemeProvider _schemeProvider;
    private readonly SignInManager _signInManager;
    private readonly UserManager _userManager;

    public LogOutModel(SignInManager signInManager,
        IIdentityServerInteractionService interaction,
        IClientStore clientStore,
        IAuthenticationSchemeProvider schemeProvider,
        IEventService events,
        ILogger<LoginModel> logger,
        UserManager userManager)
    {
        _signInManager = signInManager;
        _interaction = interaction;
        _clientStore = clientStore;
        _schemeProvider = schemeProvider;
        _events = events;
        _logger = logger;
        _userManager = userManager;
    }

    public async Task<IActionResult> OnGetAsync(string logoutId = null)
    {
        if (_signInManager.IsSignedIn(User))
        {
            await _signInManager.SignOutAsync();

            var userId = _userManager.GetUserId(User);
            var name = _userManager.GetUserName(User);

            await _events.RaiseAsync(new UserLogoutSuccessEvent(userId, name));

            var logoutUrl = "~/";

            if (logoutId != null)
            {
                var logout = await _interaction.GetLogoutContextAsync(logoutId);
                logoutUrl = logout.PostLogoutRedirectUri;
            }

            return Redirect(logoutUrl);
        }

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (_signInManager.IsSignedIn(User)) await _signInManager.SignOutAsync();

        return Redirect("~/");
    }
}