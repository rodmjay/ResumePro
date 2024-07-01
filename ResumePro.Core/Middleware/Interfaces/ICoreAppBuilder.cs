#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResumePro.Core.Settings;

namespace ResumePro.Core.Middleware.Interfaces;

public interface ICoreAppBuilder
{
    IServiceCollection Services { get; }
    AppSettings AppSettings { get; }
    IConfiguration Configuration { get; }
    string ConnectionString { get; set; }
    ICollection<string> AssembliesToMap { get; set; }
}