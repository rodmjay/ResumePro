#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography.X509Certificates;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Validation;
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
using ResumePro.Users.Managers;
using Serilog;

namespace ResumePro.Users.IdentityServer.Extensions;

public static class IdentityBuilderExtensions
{
    public static LocalIdentityServerBuilder ConfigureIdentityServer(this WebAppBuilder builder)
    {
        var identityBuilder = builder.Services.AddIdentityServer(options =>
            {
                options.IssuerUri = builder.AppSettings.Authority;
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true;

                options.UserInteraction.LoginUrl = "/Account/Login";
            })
            .AddConfigurationStore<ApplicationContext>(options =>
            {
                options.ConfigureDbContext = b => { b.UseSqlServer(builder.ConnectionString); };
            })
            .AddOperationalStore<ApplicationContext>(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(builder.ConnectionString);

                options.EnableTokenCleanup = true;
                options.TokenCleanupInterval = 30;
            })
            .AddAspNetIdentity<User>()
            .AddProfileService<IdentityProfileService>();

        if (!builder.Environment.IsDevelopment()) identityBuilder.AddConfigurationStoreCache();

        if (!string.IsNullOrWhiteSpace(builder.AppSettings.CodeSigningThumbprint))
        {
            X509Certificate2 cert = null;
            using (var certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                certStore.Open(OpenFlags.ReadOnly);
                var certCollection = certStore.Certificates.Find(
                    X509FindType.FindByThumbprint,
                    builder.AppSettings.CodeSigningThumbprint,
                    false);

                if (certCollection.Count > 0) cert = certCollection[0];
            }

            identityBuilder.AddSigningCredential(cert);
        }
        else
        {
            identityBuilder.AddDeveloperSigningCredential();
        }

        builder.Services.ConfigureApplicationCookie(config =>
        {
            config.Cookie.Name = Constants.LocalIdentity.DefaultApplicationScheme;
            config.LoginPath = "/Account/Login";
            config.LogoutPath = "/Account/Logout";
        });

        builder.Services.AddScoped<IProfileService, IdentityProfileService>();
        builder.Services.AddScoped<IResourceOwnerPasswordValidator, SignInManager>();

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

        app.UseIdentityServer();
        app.UseAuthorization();

        app.UseSession();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
            endpoints.MapRazorPages();
        });
    }
}