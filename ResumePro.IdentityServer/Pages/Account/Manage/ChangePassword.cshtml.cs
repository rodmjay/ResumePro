#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ResumePro.Users.Managers;

namespace ResumePro.IdentityServer.Pages.Account.Manage;

[Authorize("ApiScope")]
public class ChangePasswordModel : PageModel
{
    private readonly ILogger<ChangePasswordModel> _logger;
    private readonly SignInManager _signInManager;
    private readonly UserManager _userManager;

    public ChangePasswordModel(
        UserManager userManager,
        SignInManager signInManager,
        ILogger<ChangePasswordModel> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    [BindProperty] public InputModel Input { get; set; }

    [TempData] public string StatusMessage { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

        var hasPassword = await _userManager.HasPasswordAsync(user);
        if (!hasPassword) return RedirectToPage("./SetPassword");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

        var changePasswordResult =
            await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
        if (!changePasswordResult.Succeeded)
        {
            foreach (var error in changePasswordResult.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return Page();
        }

        await _signInManager.RefreshSignInAsync(user);
        _logger.LogInformation("User changed their password successfully.");
        StatusMessage = "Your password has been changed.";

        return RedirectToPage();
    }

    public class InputModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}