#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResumePro.Core.Data.Bases;

namespace ResumePro.AI.Contexts;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class ApplicationContext(DbContextOptions<ApplicationContext> options, ILoggerFactory loggerFactory)
    : BaseContext<ApplicationContext>(options)
{
    public ApplicationContext(
        DbContextOptions<ApplicationContext> options) : this(options, null)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (loggerFactory != null) optionsBuilder.UseLoggerFactory(loggerFactory);
    }

    protected override void ConfigureDatabase(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    private void SeedEntities(ModelBuilder builder)
    {
    }

    protected override void SeedDatabase(ModelBuilder builder)
    {
        SeedEntities(builder);
    }
}