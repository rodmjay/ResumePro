#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using ResumePro.Core.Middleware.Builders;
using ResumePro.Core.Settings;
using ResumePro.Users.Entities;
using ResumePro.Users.Services;
using Serilog;

namespace ResumePro.Users;

[ExcludeFromCodeCoverage]
public static class UIBuilderExtensions
{
    public static UIBuilder ConfigureUI(this WebAppBuilder builder)
    {
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services
            .AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<User>>();
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();


        return new UIBuilder(builder);
    }

    public static UIBuilder ConfigureUI(this WebAppBuilder builder, Action<RazorPagesOptions> optionsAction)
    {
        builder.Services.AddRazorPages(optionsAction);
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();


        return new UIBuilder(builder);
    }

    private static bool DisallowsSameSiteNone(string userAgent)
    {
        // Cover all iOS based browsers here. This includes:
        // - Safari on iOS 12 for iPhone, iPod Touch, iPad
        // - WkWebview on iOS 12 for iPhone, iPod Touch, iPad
        // - Chrome on iOS 12 for iPhone, iPod Touch, iPad
        // All of which are broken by SameSite=None, because they use the iOS networking stack
        if (userAgent.Contains("CPU iPhone OS 12") || userAgent.Contains("iPad; CPU OS 12")) return true;

        // Cover Mac OS X based browsers that use the Mac OS networking stack. This includes:
        // - Safari on Mac OS X.
        // This does not include:
        // - Chrome on Mac OS X
        // Because they do not use the Mac OS networking stack.
        if (userAgent.Contains("Macintosh; Intel Mac OS X 10_14") &&
            userAgent.Contains("Version/") && userAgent.Contains("Safari"))
            return true;

        // Cover Chrome 50-69, because some versions are broken by SameSite=None, 
        // and none in this range require it.
        // Note: this covers some pre-Chromium Edge versions, 
        // but pre-Chromium Edge does not require SameSite=None.
        if (userAgent.Contains("Chrome/5") || userAgent.Contains("Chrome/6")) return true;

        return false;
    }

    private static void CheckSameSite(HttpContext httpContext, CookieOptions options)
    {
        if (options.SameSite == SameSiteMode.None)
        {
            var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
            if (DisallowsSameSiteNone(userAgent))
                // For .NET Core < 3.1 set SameSite = (SameSiteMode)(-1)
                options.SameSite = SameSiteMode.Unspecified;
        }
    }

    public static UIBuilder AddCookies(this UIBuilder builder)
    {
        builder.Services.Configure<CookiePolicyOptions>(options =>
        {
            options.MinimumSameSitePolicy = SameSiteMode.None;
            options.Secure = CookieSecurePolicy.Always;
            options.OnAppendCookie = cookieContext =>
                CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
            options.OnDeleteCookie = cookieContext =>
                CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
        });
        return builder;
    }

    public static UIBuilder AddAntiForgery(this UIBuilder builder)
    {
        builder.Services.AddAntiforgery(options =>
        {
            options.SuppressXFrameOptionsHeader = true;
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        });
        return builder;
    }

    public static UIBuilder AddSession(this UIBuilder builder)
    {
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(2);
            options.Cookie.HttpOnly = true;
            options.Cookie.SameSite = SameSiteMode.None;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        });
        return builder;
    }

    public static UIBuilder AddAuthentication(this UIBuilder builder)
    {
        builder.Services.AddAuthentication(o =>
            {
                o.DefaultScheme = Constants.LocalIdentity.DefaultApplicationScheme;
                o.DefaultSignInScheme = Constants.LocalIdentity.DefaultExternalScheme;
            })
            .AddIdentityCookies(o => { });

        //builder.Services.AddAuthentication()
        //    .AddGoogle(options =>
        //    {
        //        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

        //        // register your IdentityServer with Google at https://console.developers.google.com
        //        // enable the Google+ API
        //        // set the redirect URI to https://localhost:5001/signin-google
        //        options.ClientId = "copy client ID from Google here";
        //        options.ClientSecret = "copy client secret from Google here";
        //    });

        return builder;
    }


    private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
    {
        return $"[{nameof(UIBuilderExtensions)}.{callerName}] - {message}";
    }


    public static void Build(IApplicationBuilder app, IWebHostEnvironment env,
        IOptions<AppSettings> appSettings)
    {
        Log.Logger.Debug(GetLogMessage("Configuring {applicationName}"), appSettings.Value.Name);

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
        });
    }
}