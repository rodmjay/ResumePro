#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Runtime.CompilerServices;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResumePro.Core.Data.Interfaces;
using ResumePro.Core.Services.Interfaces;
using ResumePro.Core.Settings;

namespace ResumePro.Core.Services.Bases;

public abstract class BaseService
{
    protected BaseService(IServiceProvider serviceProvider)
    {
        var loggerType = typeof(ILogger<>).MakeGenericType(GetType());
        Logger = (ILogger) serviceProvider.GetRequiredService(loggerType);
        UnitOfWork = serviceProvider.GetRequiredService<IUnitOfWorkAsync>();
        ProjectionMapping = serviceProvider.GetRequiredService<MapperConfiguration>();
        Mapper = serviceProvider.GetRequiredService<IMapper>();
        AppSettings = serviceProvider.GetRequiredService<IOptions<AppSettings>>().Value;
    }

    protected ILogger Logger { get; }

    protected AppSettings AppSettings { get; }
    public MapperConfiguration ProjectionMapping { get; }
    protected IMapper Mapper { get; }
    protected IUnitOfWorkAsync UnitOfWork { get; }


    protected string GetLogMessage(string message, [CallerMemberName] string callerName = null)
    {
        var className = GetType().Name;
        return $"[{className}.{callerName}] - {message}";
    }
}

public abstract class BaseService<TEntity> : BaseService, IService<TEntity> where TEntity : class, IObjectState
{
    protected BaseService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Repository = UnitOfWork.RepositoryAsync<TEntity>();
    }


    public IRepositoryAsync<TEntity> Repository { get; }
}