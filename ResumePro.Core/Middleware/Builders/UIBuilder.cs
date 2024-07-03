#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.Extensions.DependencyInjection;
using ResumePro.Core.Settings;

namespace ResumePro.Core.Middleware.Builders;

public class UIBuilder
{
    public UIBuilder(WebAppBuilder serviceBuilder)
    {
        Services = serviceBuilder.Services;
        AppSettings = serviceBuilder.AppSettings;
    }

    public AppSettings AppSettings { get; }
    public IServiceCollection Services { get; }
}