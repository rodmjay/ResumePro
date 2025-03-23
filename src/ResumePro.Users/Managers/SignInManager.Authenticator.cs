#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace ResumePro.Users.Managers;

public partial class SignInManager
{
    public override AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider,
        string redirectUrl,
        string userId = null)
    {
        var properties = new AuthenticationProperties {RedirectUri = redirectUrl};
        properties.Items[LoginProviderKey] = provider;
        if (userId != null) properties.Items[XsrfKey] = userId;
        return properties;
    }

    public override async Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync()
    {
        var schemes = await _schemes.GetAllSchemesAsync();
        return schemes.Where(s => !string.IsNullOrEmpty(s.DisplayName));
    }

    public override async Task<IdentityResult> UpdateExternalAuthenticationTokensAsync(
        ExternalLoginInfo externalLogin)
    {
        if (externalLogin == null) throw new ArgumentNullException(nameof(externalLogin));

        if (externalLogin.AuthenticationTokens != null && externalLogin.AuthenticationTokens.Any())
        {
            var user = await UserManager.FindByLoginAsync(externalLogin.LoginProvider, externalLogin.ProviderKey);
            if (user == null) return IdentityResult.Failed();

            foreach (var token in externalLogin.AuthenticationTokens)
            {
                var result = await UserManager.SetAuthenticationTokenAsync(user, externalLogin.LoginProvider,
                    token.Name, token.Value);
                if (!result.Succeeded) return result;
            }
        }

        return IdentityResult.Success;
    }
}