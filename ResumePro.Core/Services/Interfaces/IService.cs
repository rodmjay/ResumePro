#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Core.Data.Interfaces;

namespace ResumePro.Core.Services.Interfaces;

public interface IService<TEntity> where TEntity : class, IObjectState
{
    public MapperConfiguration ProjectionMapping { get; }
    public IRepositoryAsync<TEntity> Repository { get; }
}