#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResumePro.Users;
using ResumePro.Users.Contexts;
using ResumePro.Users.Extensions;
using ResumePro.Users.IdentityServer.Extensions;
using TechValley.Core.Extensions;
using TechValley.Data.Extensions;

namespace ResumePro.IdentityServer;

public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        Configuration = configuration;
        Environment = environment;
    }

    public IWebHostEnvironment Environment { get; }
    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        var builder = services.ConfigureApp(Configuration)
            .UseSqlServer<ApplicationContext>()
            .AddIdentity()
            .AddAutomapperProfilesFromAssemblies("ResumePro")
            .AddCaching()
            .AddUserDependencies();

        //if (Environment.IsDevelopment())
        //    builder.WithNoopEmailSender();
        //else
        //    builder.WithSendgridEmailSender();

        var webBuilder = builder.ConfigureWebApp(Environment)
            .AddAuthorization(policy => { policy.RequireAuthenticatedUser(); })
            .AddSigninDependencies();
        ;

        var idBuilder = webBuilder.ConfigureIdentityServer();

        var uiBuilder = webBuilder.ConfigureUI(options =>
            {
                options.Conventions.AuthorizeFolder("/Account/Manage", "ApiScope");
            })
            .AddCookies()
            .AddSession()
            .AddAntiForgery()
            .AddAuthentication();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationContext context)
    {
        IdentityBuilderExtensions.Configure(app, env, context);
    }
}