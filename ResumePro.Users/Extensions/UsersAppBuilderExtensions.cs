#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ResumePro.Core.Middleware.Builders;
using ResumePro.Users.Contexts;
using ResumePro.Users.Entities;
using ResumePro.Users.Factories;
using ResumePro.Users.Interfaces;
using ResumePro.Users.Managers;
using ResumePro.Users.Services;
using ResumePro.Users.Validators;

namespace ResumePro.Users.Extensions;

public static class UsersAppBuilderExtensions
{
    public static AppBuilder AddIdentity(this AppBuilder builder)
    {
        builder.Services.AddIdentityCore<User>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;

                // password requirements
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;

                options.ClaimsIdentity.EmailClaimType = "email";
                options.ClaimsIdentity.RoleClaimType = "role";
                options.ClaimsIdentity.UserIdClaimType = "sub";
                options.ClaimsIdentity.UserNameClaimType = "name";
                options.ClaimsIdentity.SecurityStampClaimType = "ss";

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(1);

                options.Stores.ProtectPersonalData = false;

                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<Role>()
            .AddRoleStore<RoleService>()
            .AddUserStore<UserService>()
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders()
            .AddDefaultUI()
            .AddClaimsPrincipalFactory<UserRoleClaimsPrincipalFactory>()
            .AddRoleManager<RoleManager>()
            .AddUserManager<UserManager>()
            .AddSignInManager<SignInManager<User>>();

        builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
            opt.TokenLifespan = TimeSpan.FromHours(24));


        return builder;
    }

    public static AppBuilder AddUserDependencies(this AppBuilder builder)
    {
        builder.Services.TryAddScoped<IUserService, UserService>();
        builder.Services.TryAddScoped<IRoleService, RoleService>();

        builder.Services.TryAddScoped<IUserClaimsPrincipalFactory<User>, UserRoleClaimsPrincipalFactory>();
        builder.Services.TryAddScoped<UserRoleClaimsPrincipalFactory>();

        builder.Services.TryAddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
        builder.Services.TryAddScoped<IUserConfirmation<User>, DefaultUserConfirmation<User>>();
        builder.Services.TryAddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();

        builder.Services.TryAddScoped<IdentityErrorDescriber>();

        builder.Services.TryAddScoped<UserManager<User>, UserManager>();
        builder.Services.TryAddScoped<UserManager>();

        builder.Services.TryAddScoped<RoleManager<Role>, RoleManager>();
        builder.Services.TryAddScoped<RoleManager>();

        builder.Services.TryAddScoped<SignInManager<User>, SignInManager>();
        builder.Services.TryAddScoped<SignInManager>();

        builder.Services.TryAddTransient<IUserValidator<User>, DuplicateEmailValidator>();
        builder.Services.TryAddTransient<IUserValidator<User>, DuplicateUserNameValidator>();

        builder.Services.TryAddScoped<IUserAccessor, UserAccessor>();


        return builder;
    }

    public static WebAppBuilder AddSigninDependencies(this WebAppBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();

        builder.Services.TryAddScoped<SignInManager<User>, SignInManager>();
        builder.Services.TryAddScoped<SignInManager>();

        return builder;
    }
}