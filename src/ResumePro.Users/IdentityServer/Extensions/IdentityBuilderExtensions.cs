#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ResumePro.Core.Middleware.Builders;
using ResumePro.Users.Contexts;
using ResumePro.Users.Entities;
using ResumePro.Users.IdentityServer.Builders;
using ResumePro.Users.IdentityServer.Services;
using ResumePro.Users.Interfaces;
using ResumePro.Users.Managers;
using Serilog;

namespace ResumePro.Users.IdentityServer.Extensions;

public static class IdentityBuilderExtensions
{
    public static LocalIdentityServerBuilder ConfigureIdentityServer(this WebAppBuilder builder)
    {
        // Replace Duende IdentityServer with a custom implementation
        // This is a placeholder for the actual implementation
        
        builder.Services.AddScoped<ILocalProfileService, IdentityProfileService>();
        builder.Services.AddScoped<ILocalResourceOwnerPasswordValidator, SignInManager>();

        builder.Services.ConfigureApplicationCookie(config =>
        {
            config.Cookie.Name = Constants.LocalIdentity.DefaultApplicationScheme;
            config.LoginPath = "/Account/Login";
            config.LogoutPath = "/Account/Logout";
        });

        builder.Services.AddAntiforgery(options =>
        {
            options.SuppressXFrameOptionsHeader = true;
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        });

        return new LocalIdentityServerBuilder(builder);
    }

    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env,
        ApplicationContext context)
    {
        context.Database.Migrate();

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseXXssProtection(options => options.EnabledWithBlockMode());

        app.UseSerilogRequestLogging();

        app.UseStaticFiles();
        app.UseRouting();

        // Replace UseIdentityServer with custom middleware if needed
        app.UseAuthorization();

        app.UseSession();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
            endpoints.MapRazorPages();
        });
    }
}
