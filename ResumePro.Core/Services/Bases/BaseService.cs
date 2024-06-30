#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ResumePro.Core.Data.Interfaces;
using ResumePro.Core.Services.Interfaces;
using ResumePro.Core.Settings;

namespace ResumePro.Core.Services.Bases;

public abstract class BaseService
{
    protected BaseService(IServiceProvider serviceProvider)
    {
        UnitOfWork = serviceProvider.GetRequiredService<IUnitOfWorkAsync>();
        ProjectionMapping = serviceProvider.GetRequiredService<MapperConfiguration>();
        Mapper = serviceProvider.GetRequiredService<IMapper>();
        AppSettings = serviceProvider.GetRequiredService<IOptions<AppSettings>>().Value;
    }

    protected AppSettings AppSettings { get; }
    public MapperConfiguration ProjectionMapping { get; }
    protected IMapper Mapper { get; }
    protected IUnitOfWorkAsync UnitOfWork { get; }
}

public abstract class BaseService<TEntity> : BaseService, IService<TEntity> where TEntity : class, IObjectState
{

    protected BaseService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Repository = UnitOfWork.RepositoryAsync<TEntity>();
    }

    public IRepositoryAsync<TEntity> Repository { get; }
}