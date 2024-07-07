#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumePro.Users.Entities;
using ResumePro.Users.Managers;

namespace ResumePro.IdentityServer.Pages.Account.Manage;

public class IndexModel : PageModel
{
    private readonly SignInManager _signInManager;
    private readonly UserManager _userManager;

    public IndexModel(
        UserManager userManager,
        SignInManager signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public string Username { get; set; }

    [TempData] public string StatusMessage { get; set; }

    [BindProperty] public InputModel Input { get; set; }

    private async Task LoadAsync(User user)
    {
        var userName = await _userManager.GetUserNameAsync(user);

        Username = userName;

        Input = new InputModel();
        Input.FirstName = user.FirstName;
        Input.LastName = user.LastName;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

        await LoadAsync(user);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

        if (!ModelState.IsValid)
        {
            await LoadAsync(user);
            return Page();
        }

        user.FirstName = Input.FirstName;
        user.LastName = Input.LastName;


        var updateUserResult = await _userManager.UpdateAsync(user);
        if (!updateUserResult.Succeeded)
        {
            StatusMessage = "Unexpected error when trying to update profile.";
            return RedirectToPage();
        }

        await _signInManager.RefreshSignInAsync(user);
        StatusMessage = "Your profile has been updated";
        return RedirectToPage();
    }

    public class InputModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}